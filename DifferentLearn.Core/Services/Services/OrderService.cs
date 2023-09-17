using DifferentLearn.Core.DTOs.Order;
using DifferentLearn.Core.Services.Interfaces;
using DifferentLearn.Data.Contexts;
using DifferentLearn.Data.Entites.Course;
using DifferentLearn.Data.Entites.Order;
using DifferentLearn.Data.Entites.User;
using DifferentLearn.Data.Entites.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Services.Services
{
    public class OrderService : IOrderService
    {
        private DiffLearnContext _context;
        private IUserService _userService;
        private ICourseService _courseService;
        private IWalletService _walletService;
        public OrderService(DiffLearnContext context, IUserService userService, ICourseService courseService, IWalletService walletService)
        {
            _context = context;
            _userService = userService;
            _courseService = courseService;
            _walletService = walletService;

        }

        public async Task AddDiscountAsync(DisCount disCount)
        {
            await _context.DisCounts.AddAsync(disCount);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddOrderAsync(string username, int courseid)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid = user.UserId;


            Order order = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userid && o.IsFinaly == false);

            var course = await _courseService.GetCourseByIdAsync(courseid);

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userid,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId=courseid,
                            Count=1,
                            Price=course.CoursePrice,
                        }
                    }
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                OrderDetail detail = await _context.OrderDetails.FirstOrDefaultAsync(d => d.OrderId == order.OrderId && d.CourseId == courseid);
                if (detail != null)
                {
                    detail.Count += 1;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        CourseId = courseid,
                        Price = course.CoursePrice,
                    };
                    _context.OrderDetails.Add(detail);
                }

                await _context.SaveChangesAsync();
                await UpdatePriceOrderAsync(order.OrderId);
            }

            return order.OrderId;
        }

        public Task<bool> FinalyOrderAsync(int orderid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FinalyOrderAsync(string username, int orderid)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid = user.UserId;
            Order order = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Course)
                .FirstOrDefaultAsync(o => o.OrderId == orderid && o.UserId == userid);
            if (order == null || order.IsFinaly == true)
            {
                return false;
            }
            if (await _walletService.BalanceUserWalletAsync(username) >= order.OrderSum)
            {
                order.IsFinaly = true;
                _walletService.AddWalletAsync(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    Description = "فاکتور شماره #" + order.OrderId,
                    UserId = userid,
                    TypeId = 2
                });
                _context.Orders.Update(order);

                foreach (var item in order.OrderDetails)
                {
                    await _context.UserCourses.AddAsync(new UserCourse()
                    {
                        CourseId = item.CourseId,
                        UserId = userid
                    });
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<DisCount>> GetAllDiscountsAsync()
        {
            return await _context.DisCounts.ToListAsync();
        }

        public async Task<DisCount> GetDiscountForEditAsync(int discountid)
        {
           DisCount dis= await _context.DisCounts.FindAsync(discountid);
            return dis;
        }

        public async Task<Order> GetOrderByIdAsync(int orderid)
        {
            return await _context.Orders.FindAsync(orderid);
        }

        public async Task<Order> GetOrderForUserPanelAsync(string username, int orderid)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid = user.UserId;
            return await _context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Course)
                .FirstOrDefaultAsync(o => o.UserId == userid && o.OrderId == orderid);

        }

        public async Task<int> GetUserOrderAsync(string username)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid = user.UserId;
            Order order = await _context.Orders.SingleOrDefaultAsync(o => o.UserId == userid);
            return order.OrderId;
        }

        public async Task<List<Order>> GetUserOrdersAsync(string username)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid = user.UserId;
            return await _context.Orders.Where(o => o.UserId == userid).ToListAsync();
        }

        public async Task<bool> IsExistCodeAsync(string code)
        {
            return await _context.DisCounts.AnyAsync(d=>d.DiscountCode==code);
        }

        public async Task<bool> IsUserInCourseAsync(string username, int courseid)
        {
            User user = await _userService.GetUserByUserNameAsync(username);
            int userid=user.UserId;
            return await _context.UserCourses.AnyAsync(c => c.UserId == userid && c.CourseId == courseid);
        }

        public async Task UpdateDiscountAsync(DisCount disCount)
        {
            _context.DisCounts.Update(disCount);
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriceOrderAsync(int orderid)
        {
            Order order = await _context.Orders.FindAsync(orderid);
            order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderid).Sum(d => d.Price * d.Count);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<DisCountUseType> UseDisCountAsync(int orderid, string code)
        {
            var discount = await _context.DisCounts.SingleOrDefaultAsync(d => d.DiscountCode == code);
            if (discount == null)
            {
                return DisCountUseType.NotFound;
            }

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
            {
                return DisCountUseType.ExpierDate;
            }
            if (discount.EndDate != null && discount.StartDate >= DateTime.Now)
            {
                return DisCountUseType.ExpierDate;
            }

            if (discount.UsableCount != null && discount.UsableCount < 1)
            {
                return DisCountUseType.Finished;
            }

            var order = await GetOrderByIdAsync(orderid);
            if (_context.UserDisCountCodes.Any(d=>d.UserId==order.UserId&&d.DiscountId==discount.DiscountId))
            {
                return DisCountUseType.UserUsed;
            }
            int percent = (order.OrderSum * discount.DisCountPercent) / 100;
            order.OrderSum = order.OrderSum - percent;
            await UpdateOrderAsync(order);
            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }
            _context.DisCounts.Update(discount);
            _context.UserDisCountCodes.Add(new UserDisCountCode()
            {
                UserId=order.UserId,
                DiscountId=discount.DiscountId
            });
            await _context.SaveChangesAsync();
            return DisCountUseType.Success;
        }
    }
}

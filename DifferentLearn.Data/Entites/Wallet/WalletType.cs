using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Data.Entites.Wallet
{
    public class WalletType
    {
        public WalletType()
        {
            
        }
        [Key,DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }
        [Required]
        [MaxLength(150)]
        public required string TypeTitle { get; set; }


        #region Relations
        public  List<Wallet>? Wallets { get; set; }
        #endregion
    }
}

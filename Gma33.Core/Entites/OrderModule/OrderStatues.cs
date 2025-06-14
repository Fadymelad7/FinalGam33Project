using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.OrderModule
{
    public enum OrderStatues
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "PaymentReceived")]
        PaymentReceived,

        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed


    }
}

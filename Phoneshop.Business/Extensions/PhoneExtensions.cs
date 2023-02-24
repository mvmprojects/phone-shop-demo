using Phoneshop.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class PhoneExtensions
    {
        public static decimal PriceWithoutVAT(this Phone phone)
        {
            return phone.Price / 1.21M;
        }

        public static string FullName(this Phone phone)
        {
            return $"{((phone.Brand == null) ? string.Empty : phone.Brand.Name)} - {phone.Type}";
        }
    }
}

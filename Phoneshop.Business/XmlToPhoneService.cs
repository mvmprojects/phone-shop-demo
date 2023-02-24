using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Phoneshop.Business
{
    public class XmlToPhoneService : IXmlToPhoneService
    {
        private readonly IPhoneService _phoneService;

        #region
        public XmlToPhoneService(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }
        #endregion

        /// <summary>
        /// Expects XML document in string form before mapping the data.
        /// </summary>
        /// <param name="doc"></param>
        public void MapImportedPhones(string doc)
        {
            int cnt = 0;
            int validCnt = 0;

            // replace excessive whitespace with a single space character
            // ( combine with .Trim() later )
            string clean = Regex.Replace(doc, @"\s+", " ");

            using var reader = XmlReader.Create(new StringReader(clean));

            var item = new Phone() { Brand = new Brand() };

            try
            {
                while (reader.Read())
                {
                    if (reader.Name != "Phones" &&
                        reader.Name != "Phone" &&
                        reader.IsStartElement())
                    {
                        MapPhoneData(reader, item);
                    }

                    if (reader.NodeType == XmlNodeType.EndElement &&
                        reader.Name == "Phone")
                    {
                        var result = _phoneService.CreatePhone(item);

                        cnt++;
                        if (result != null && result.Id > 0) validCnt++;

                        // clear all fields on item Phone object
                        // in case XML has empty nodes.
                        // NOTE: EF will attach id values to var "item" in the background
                        // so those values have to be cleared as well!
                        // create a fresh object to prevent issues:
                        item = new Phone() { Brand = new Brand() };
                    }
                }
                Console.WriteLine($"Service went through {cnt} item(s)." +
                    $"\nSuccessful insertion of {validCnt} item(s).");
            }
            catch (XmlException)
            {
                Console.WriteLine("An XML parsing error has occurred.");
                throw;
            }
        }

        private void MapPhoneData(XmlReader reader, Phone item)
        {
            switch (reader.Name)
            {
                case "Brand":
                    item.Brand.Name = reader.ReadElementContentAsString();
                    break;
                case "Type":
                    item.Type = reader.ReadElementContentAsString();
                    break;
                case "Price":
                    item.Price = reader.ReadElementContentAsDecimal();
                    break;
                case "Description":
                    item.Description = reader.ReadElementContentAsString().Trim();
                    break;
                case "Stock":
                    item.Stock = reader.ReadElementContentAsInt();
                    break;
                default:
                    break;
            }
        }
    }
}

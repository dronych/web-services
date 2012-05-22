using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintVendorList
{
    // Import newly generated Web service proxy.
    using PrintVendorList.NAVService;

    class Program
    {
        static void Main(string[] args)
        {
            // Create instance of service and set credentials.
            Vendor_Binding service = new Vendor_Binding();
            service.UseDefaultCredentials = true;
            // Create instance of vendor.
            Vendor vend = new Vendor();
            vend.Name = "Vendor Name";
            Msg("Pre Create");
            PrintVendor(vend);

            // Insert vendor.
            service.Create(ref vend);
            Msg("Post Create");
            PrintVendor(vend);

            // Create filter for searching for vendors.
            List<Vendor_Filter> filterArray = new List<Vendor_Filter>();
            Vendor_Filter nameFilter = new Vendor_Filter();
            nameFilter.Field = Vendor_Fields.Name;
            nameFilter.Criteria = "C*";
            filterArray.Add(nameFilter);

            Msg("List before modification");
            PrintVendorList(service, filterArray);

            vend.Name = vend.Name + "Updated";
            service.Update(ref vend);

            Msg("Post Update");
            PrintVendor(vend);

            Msg("List after modification");
            PrintVendorList(service, filterArray);
            service.Delete(vend.Key);

            Msg("List after deletion");
            PrintVendorList(service, filterArray);

            Msg("Press [ENTER] to exit program!");
            Console.ReadLine();
        }

        // Print list of filtered vendors.
        static void PrintVendorList(Vendor_Binding service, List<Vendor_Filter> filter)
        {
            Msg("Printing Vendor List");

            // Run the actual search.
            Vendor[] list = service.ReadMultiple(filter.ToArray(), null, 100);
            foreach (Vendor v in list)
            {
                PrintVendor(v);
            }
            Msg("End of List");
        }

        static void PrintVendor(Vendor v)
        {
            Console.WriteLine("No: {0} Name: {1}", v.No, v.Name);
        }

        static void Msg(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
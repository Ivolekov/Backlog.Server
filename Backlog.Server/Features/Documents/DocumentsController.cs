namespace Backlog.Server.Features.Documents
{
    using AspNetCore.Reporting;
    using Backlog.Server.Features.Company;
    using Backlog.Server.Features.ServiceProtocols;
    using Backlog.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    [Authorize]
    public class DocumentsController : ApiController
    {
        private readonly IServiceProtocolService serviceProtocolService;
        private readonly ICompanyService companyService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DocumentsController(IServiceProtocolService serviceProtocolService, ICompanyService companyService, IWebHostEnvironment webHostEnvironment)
        {
            this.serviceProtocolService = serviceProtocolService;
            this.companyService = companyService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route(nameof(CreateServiceProtocolDocument) + "/{id}")]
        [HttpGet]
        public async Task<IActionResult> CreateServiceProtocolDocument(int id)
        {
            try
            {
                var path = $"{this.webHostEnvironment.ContentRootPath}\\DocumentTemplates\\ServiceProtocol.rdlc";
                var serviceProtocol = await serviceProtocolService.GetServiceProtocolById(id);
                var companyProfile = await companyService.GetCompanyProfile(this.User.GetId());
                if (companyProfile == null)
                {
                    companyProfile = new ServiceManager.Models.CompanyProfile()
                    {
                        CompanyName = "Company",
                        CompanyAddress = "Address",
                        CompanyEmail = "mail@mail.com",
                        CompanyPhone = "+35900889966"
                    };
                }
                var dt = new DataTable();

                dt.Columns.Add("Id");
                dt.Columns.Add("ClientPhone");
                dt.Columns.Add("ClientEmail");
                dt.Columns.Add("BrandModel");
                dt.Columns.Add("SimTray");
                dt.Columns.Add("Charger");
                dt.Columns.Add("Bag");
                dt.Columns.Add("Other");
                dt.Columns.Add("Pin");
                dt.Columns.Add("UnlockPass");
                dt.Columns.Add("LockPattern");
                dt.Columns.Add("C_DataTime");
                dt.Columns.Add("Comment");
                dt.Columns.Add("ServiceAction");
                dt.Columns.Add("FaultInformation");
                dt.Columns.Add("FinalPrice");

                DataRow row = dt.NewRow();
                row["Id"] = serviceProtocol.Id;
                row["ClientPhone"] = serviceProtocol.ClientPhone;
                row["ClientEmail"] = serviceProtocol.ClientEmail;
                row["BrandModel"] = serviceProtocol.BrandModel;
                row["SimTray"] = serviceProtocol.SimTray == false ? "" : "Да";
                row["Charger"] = serviceProtocol.Charger == false ? "" : "Да";
                row["Bag"] = serviceProtocol.Bag == false ? "" : "Да";
                row["Other"] = serviceProtocol.Other;
                row["Pin"] = serviceProtocol.Pin;
                row["UnlockPass"] = serviceProtocol.UnlockPass;
                row["LockPattern"] = serviceProtocol.LockPattern;
                row["C_DataTime"] = serviceProtocol.C_DateTime != null ? serviceProtocol.C_DateTime.Value.ToString("dd.MM.yyyy") : serviceProtocol.C_DateTime;
                row["Comment"] = serviceProtocol.Comment;
                row["ServiceAction"] = serviceProtocol.ServiceAction;
                row["FaultInformation"] = serviceProtocol.FaultInformation;
                row["FinalPrice"] = serviceProtocol.FinalPrice != null ? serviceProtocol.FinalPrice.Value.ToString("0.00") + "лв." : serviceProtocol.FinalPrice;
                dt.Rows.Add(row);

                LocalReport localReport = new LocalReport(path);

                localReport.AddDataSource("dsServiceProtocol", dt);

                dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Address");
                dt.Columns.Add("Phone");
                dt.Columns.Add("Email");
                row = dt.NewRow();
                row["Name"] = companyProfile.CompanyName;
                row["Address"] = companyProfile.CompanyAddress;
                row["Phone"] = companyProfile.CompanyPhone;
                row["Email"] = companyProfile.CompanyEmail;

                dt.Rows.Add(row);

                localReport.AddDataSource("dsCompanyProfile", dt);
                var result = localReport.Execute(RenderType.Pdf, 1).MainStream;
                return File(result, "application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route(nameof(CreateServiceProtocolWarrantyCardDocument) + "/{id}/{period}")]
        [HttpGet]
        public async Task<IActionResult> CreateServiceProtocolWarrantyCardDocument(int id, double period)
        {
            try
            {
                var serviceProtocol = await serviceProtocolService.GetServiceProtocolById(id);
                var companyProfile = await companyService.GetCompanyProfile(this.User.GetId());
                if (companyProfile == null)
                {
                    companyProfile = new ServiceManager.Models.CompanyProfile()
                    {
                        CompanyName = "Company",
                        CompanyAddress = "Address",
                        CompanyEmail = "mail@mail.com",
                        CompanyPhone = "+35900889966"
                    };
                }
                var path = $"{this.webHostEnvironment.ContentRootPath}\\DocumentTemplates\\WarrantyCardServiceProtocol.rdlc";
                var service = $"{serviceProtocol.BrandModel} - {serviceProtocol.ServiceAction}";
                var finalPrice = serviceProtocol.FinalPrice ?? 0.00m;
                if (string.IsNullOrEmpty(serviceProtocol.ServiceAction))
                {
                    service = serviceProtocol.BrandModel;
                }

                var dt = new DataTable();
                dt.Columns.Add("Service");
                dt.Columns.Add("Date");
                dt.Columns.Add("Price");
                dt.Columns.Add("WarrantyPeriod");

                DataRow row = dt.NewRow();
                row["Service"] = service;
                row["Date"] = DateTime.Now.ToString("dd.MM.yyyy");
                row["Price"] = finalPrice.ToString("0.00") + "лв.";
                row["WarrantyPeriod"] = period.ToString() + "м.";
                dt.Rows.Add(row);

                LocalReport localReport = new LocalReport(path);
                localReport.AddDataSource("dsSPWarrantyCard", dt);

                dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Address");
                dt.Columns.Add("Phone");
                dt.Columns.Add("Email");
                row = dt.NewRow();
                row["Name"] = companyProfile.CompanyName;
                row["Address"] = companyProfile.CompanyAddress;
                row["Phone"] = companyProfile.CompanyPhone;
                row["Email"] = companyProfile.CompanyEmail;
                dt.Rows.Add(row);

                localReport.AddDataSource("dsCompanyProfile", dt);

                var result = localReport.Execute(RenderType.Pdf, 1).MainStream;
                return File(result, "application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

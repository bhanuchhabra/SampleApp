using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SalaryContracts;
using SalaryWebApp.Controllers;
using SalaryWebApp.Models;

namespace WebAppUnitTests
{
    [TestClass]
    public class WebAppTest
    {

        [TestMethod]
        public void PositiveTest_GetEmployee_EmployeeIsCreated()
        {
            var employeeControllerSUT = new EmployeeController(new ProxyEmployeeRepository());

            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();

            employeeMock1.Setup(emp => emp.FirstName).Returns("TestName1");

            employeeControllerSUT.Post(employeeMock1.Object as Employee);

            var fromGet = employeeControllerSUT.Get().Content;

            Assert.AreEqual(1, fromGet.Count);
            Assert.IsTrue(fromGet[0].FirstName.Equals(employeeMock1.Object.FirstName));
        }

        [TestMethod]
        public void PositiveTest_GetEmployee_FromPassedRepo_EmployeesAreCreated()
        {

            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            var employeeMock2 = new Mock<Employee>().As<ITaxableEmployee>();

            employeeMock1.Setup(emp => emp.FirstName).Returns("TestName1");
            employeeMock2.Setup(emp => emp.FirstName).Returns("TestName2");
            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>();

            taxableEmployees.Add(employeeMock1.Object);
            taxableEmployees.Add(employeeMock2.Object);
            var mockRepo = new Mock<IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);

            var employeeControllerSUT = new EmployeeController(mockRepo.Object);

            var fromGet = employeeControllerSUT.Get().Content;

            Assert.AreEqual(2, fromGet.Count);
            Assert.IsTrue(fromGet[0].FirstName.Equals(employeeMock1.Object.FirstName));
            Assert.IsTrue(fromGet[1].FirstName.Equals(employeeMock2.Object.FirstName));

        }

        [TestMethod]
        public void PositiveTest_GetEmployee_ByCorrectId_EmployeeFound()
        {
            var employeeControllerSUT = new EmployeeController(new ProxyEmployeeRepository());

            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            var employeeMock2 = new Mock<Employee>().As<ITaxableEmployee>();

            employeeMock1.Setup(emp => emp.FirstName).Returns("TestName1");
            employeeMock2.Setup(emp => emp.FirstName).Returns("TestName2");

            employeeControllerSUT.Post(employeeMock1.Object as Employee);
            employeeControllerSUT.Post(employeeMock2.Object as Employee);

            var fromGet = employeeControllerSUT.Get().Content;

            Assert.AreEqual(fromGet.Count, 2);
            Assert.IsTrue(fromGet[0].FirstName.Equals(employeeMock1.Object.FirstName));
            Assert.IsTrue(fromGet[1].FirstName.Equals(employeeMock2.Object.FirstName));
        }

        [TestMethod]
        public void NegativeTest_GetEmployee_ByWrongId_NoSuchEmployee()
        {
            var employeeControllerSUT = new EmployeeController(new ProxyEmployeeRepository());

            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            var employeeMock2 = new Mock<Employee>().As<ITaxableEmployee>();

            employeeMock1.Setup(emp => emp.FirstName).Returns("TestName1");
            employeeMock2.Setup(emp => emp.FirstName).Returns("TestName2");

            employeeControllerSUT.Post(employeeMock1.Object as Employee);
            employeeControllerSUT.Post(employeeMock2.Object as Employee);

            var fromGet = employeeControllerSUT.Get(3).Content;

            Assert.IsNull(fromGet);
        }

        [TestMethod]
        public void PositiveTest_GetEmployee_ByCorrectId_OfSecondEmployee_EmployeeFound()
        {
            var employeeControllerSUT = new EmployeeController(new ProxyEmployeeRepository());

            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            var employeeMock2 = new Mock<Employee>().As<ITaxableEmployee>();

            employeeMock1.Setup(emp => emp.FirstName).Returns("TestName1");
            employeeMock2.Setup(emp => emp.FirstName).Returns("TestName2");

            employeeControllerSUT.Post(employeeMock1.Object as Employee);
            employeeControllerSUT.Post(employeeMock2.Object as Employee);

            var fromGet = employeeControllerSUT.Get(2).Content;

            Assert.AreEqual(employeeMock2.Object.FirstName, fromGet.FirstName);
            Assert.AreNotEqual(employeeMock1.Object.FirstName, fromGet.FirstName);

        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(60050);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(9);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>();
            taxableEmployees.Add(employeeMock1.Object);

            var mockRepo = new Mock<IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(0);
            var paySlip = (getResponse as System.Web.Http.Results.JsonResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(5004, paySlip.GrossIncome);
            Assert.AreEqual(922, paySlip.IncomeTax);
            Assert.AreEqual(4082, paySlip.NetIncome);
            Assert.AreEqual(450, paySlip.SuperAmount);
        }

        [TestMethod]
        public void NegativeTest_GetPayslip_ByWrongId_CorrectTaxCalculation()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(60050);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(9);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>();
            taxableEmployees.Add(employeeMock1.Object);

            var mockRepo = new Mock<SalaryWebApp.Models.IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(1);
            Assert.AreEqual("No such employee", (getResponse as System.Web.Http.Results.JsonResult<EmptyResult>).Content.Message);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_2()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(120000);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(10);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>();
            taxableEmployees.Add(employeeMock1.Object);

            var mockRepo = new Mock<SalaryWebApp.Models.IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(0);
            var paySlip = (getResponse as System.Web.Http.Results.JsonResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(10000, paySlip.GrossIncome);
            Assert.AreEqual(2669, paySlip.IncomeTax);
            Assert.AreEqual(7331, paySlip.NetIncome);
            Assert.AreEqual(1000, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_3()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(20000);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(12);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>
            {
                employeeMock1.Object
            };

            var mockRepo = new Mock<IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(0);
            var paySlip = (getResponse as System.Web.Http.Results.JsonResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(1667, paySlip.GrossIncome);
            Assert.AreEqual(29, paySlip.IncomeTax);
            Assert.AreEqual(1638, paySlip.NetIncome);
            Assert.AreEqual(200, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_5()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(9000);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(0);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>
            {
                employeeMock1.Object
            };

            var mockRepo = new Mock<IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(0);
            var paySlip = (getResponse as System.Web.Http.Results.JsonResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(750, paySlip.GrossIncome);
            Assert.AreEqual(0, paySlip.IncomeTax);
            Assert.AreEqual(750, paySlip.NetIncome);
            Assert.AreEqual(0, paySlip.SuperAmount);
        }


        [TestMethod]
        public void PositiveTest_GetPayslip_ByCorrectId_CorrectTaxCalculation_4()
        {
            var employeeMock1 = new Mock<Employee>().As<ITaxableEmployee>();
            employeeMock1.Setup(emp => emp.FirstName).Returns("FirstName");
            employeeMock1.Setup(emp => emp.LastName).Returns("LastName");
            employeeMock1.Setup(emp => emp.PaymentStartDate).Returns(new DateTime(2018, 03, 01));
            employeeMock1.Setup(emp => emp.AnualSalary).Returns(200000);
            employeeMock1.Setup(emp => emp.SuperRate).Returns(11);

            List<ITaxableEmployee> taxableEmployees = new List<ITaxableEmployee>
            {
                employeeMock1.Object
            };

            var mockRepo = new Mock<IProxyRepo<ITaxableEmployee>>();
            mockRepo.Setup(x => x.Entities).Returns(taxableEmployees);
            var SUT = new PayslipController(mockRepo.Object);

            var getResponse = SUT.Get(0);
            var paySlip = (getResponse as System.Web.Http.Results.JsonResult<IPayslip>).Content;

            Assert.IsNotNull(paySlip);
            Assert.AreEqual(16667, paySlip.GrossIncome);
            Assert.AreEqual(5269, paySlip.IncomeTax);
            Assert.AreEqual(11398, paySlip.NetIncome);
            Assert.AreEqual(1833, paySlip.SuperAmount);
        }

        [TestMethod]
        public void PositiveTest_GetPayslip_WithNoId_APIReferenceStringReturned()
        {
            var SUT = new PayslipController();
            var getResponse = SUT.Get();

            Assert.AreEqual("user api/Payslip/id to get payslip of Employee", (getResponse as System.Web.Http.Results.JsonResult<EmptyResult>).Content.Message);
        }

    }
}

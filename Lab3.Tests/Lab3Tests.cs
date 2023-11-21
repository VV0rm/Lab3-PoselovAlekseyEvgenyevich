using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Lab3.Model;

[TestClass]
public class Lab3Tests
{
    [TestMethod]
    public void ProcessData_ExistingUser_ReturnsExpectedResult()
    {
        // Arrange
        var databaseService = new Mock<IDatabaseService>();
        databaseService.Setup(d => d.CheckDataExists("506_StX", "732%ЗоВ*", "732%ЗоВ*")).Returns(true);

        var userInteractionService = new Mock<IUserInteractionService>();
        userInteractionService.Setup(u => u.ShowMessage()).Returns("Входные данные получены");

        var dataTransferService = new Mock<IDataTransferService>();

        BusinessLogicController controller = new BusinessLogicController(databaseService.Object, dataTransferService.Object, userInteractionService.Object);

        // Act
        string result = controller.ProcessData();

        // Assert
        Assert.AreEqual("Операция выполнена", result);
        dataTransferService.Verify(d => d.SendData(It.IsAny<string>()), Times.Once());
    }

    [TestMethod]
    public void ProcessData_NewUser_ReturnsExpectedResult()
    {
        // Arrange
        var databaseService = new Mock<IDatabaseService>();
        databaseService.Setup(d => d.CheckDataExists("8610_tUb", "512?ДоЛ#", "512?ДоЛ#")).Returns(false);

        var userInteractionService = new Mock<IUserInteractionService>();
        userInteractionService.Setup(u => u.ShowMessage()).Returns("Входные данные получены");

        var dataTransferService = new Mock<IDataTransferService>();

        BusinessLogicController controller = new BusinessLogicController(databaseService.Object, dataTransferService.Object, userInteractionService.Object);

        // Act
        string result = controller.ProcessData();

        // Assert
        Assert.AreEqual("Операция выполнена", result);
        databaseService.Verify(d => d.AddDataToDatabase(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        dataTransferService.Verify(d => d.SendData(It.IsAny<string>()), Times.Once());
    }

    [TestMethod]
    public void ProcessData_InputFromUser_SuccessfullyObtainsUserInput()
    {
        // Arrange
        var databaseService = new Mock<IDatabaseService>();
        var userInteractionService = new Mock<IUserInteractionService>();
        userInteractionService.Setup(u => u.ShowMessage()).Returns("Входные данные получены");

        var dataTransferService = new Mock<IDataTransferService>();

        BusinessLogicController controller = new BusinessLogicController(databaseService.Object, dataTransferService.Object, userInteractionService.Object);

        // Act
        string result = controller.ProcessData();

        // Assert
        Assert.AreEqual("Операция выполнена", result);
        userInteractionService.Verify(u => u.ShowMessage(), Times.Once());
    }

    [TestMethod]
    public void ProcessData_SendDataToExternalDependency_SuccessfullySendsData()
    {
        // Arrange
        var databaseService = new Mock<IDatabaseService>();
        databaseService.Setup(d => d.CheckDataExists("506_StX", "732%ЗоВ*", "732%ЗоВ*")).Returns(true);

        var userInteractionService = new Mock<IUserInteractionService>();
        userInteractionService.Setup(u => u.ShowMessage()).Returns("Входные данные получены");

        var dataTransferService = new Mock<IDataTransferService>();

        BusinessLogicController controller = new BusinessLogicController(databaseService.Object, dataTransferService.Object, userInteractionService.Object);

        // Act
        string result = controller.ProcessData();

        // Assert
        dataTransferService.Verify(d => d.SendData("Данные получены из базы данных"), Times.Once);
    }

    [TestMethod]
    public void ProcessData_IntegrationTest_FullRegistrationFlow()
    {
        // Arrange
        var databaseService = new Mock<IDatabaseService>();
        var userInteractionService = new Mock<IUserInteractionService>();
        var dataTransferService = new Mock<IDataTransferService>();

        BusinessLogicController controller = new BusinessLogicController(databaseService.Object, dataTransferService.Object, userInteractionService.Object);

        // Act
        string result = controller.ProcessData();

        // Assert
        Assert.IsNotNull(result);
    }
}

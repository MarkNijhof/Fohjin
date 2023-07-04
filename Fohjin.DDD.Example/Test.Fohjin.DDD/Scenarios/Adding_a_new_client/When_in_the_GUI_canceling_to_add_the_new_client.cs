using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client;

public class When_in_the_GUI_canceling_to_add_the_new_client : PresenterTestFixture<ClientDetailsPresenter>
{
    private readonly Guid _clientId = Guid.NewGuid();
    private ClientDetailsReport _clientDetailsReport = null!;
    private List<ClientDetailsReport>? _clientDetailsReports;

    protected override void SetupDependencies()
    {
        _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "street", "123", "5000", "bergen", "1234567890");
        _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
        OnDependency<IReportingRepository>()
            .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
            .Returns(_clientDetailsReports);
    }

    protected override void Given()
    {
        Presenter.SetClient(null);
        Presenter.Display();
        On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("Client name");
        On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientNameChange += null);
    }

    protected override void When()
    {
        On<IClientDetailsView>().FireEvent(x => x.OnCancel += null);
    }

    [TestMethod]
    public void Then_the_view_will_be_closed()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.Close()).WasCalled();
    }
}
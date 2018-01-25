using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockExchange.Data;
using StockExchange.Domain;

namespace StockExchange.Repo.Test
{
  [TestClass]
  public class GenericRepositoryIntegrationTests
  {
    private StringBuilder _logBuilder = new StringBuilder();
    private string _log;
    private StockContext _context;
    private GenericRepository<ActiveStock> _activeStockRepository;

    public GenericRepositoryIntegrationTests() {
      Database.SetInitializer(new NullDatabaseInitializer<StockContext>());
      _context = new StockContext();
      _activeStockRepository = new GenericRepository<ActiveStock>(_context);
      SetupLogging();
    }

    [TestMethod]
    public void CanFindByActiveStockByKeyWithDynamicLambda() {
      _activeStockRepository.FindByKey(1);
      WriteLog();
      Assert.IsTrue(_log.Contains("FROM [sm].[ActiveStocks"));
    }

    [TestMethod]
    public void CanFindByCompanyByKeyWithDynamicLambda() {
      new GenericRepository<Company>(_context).FindByKey(1);
      WriteLog();
      Assert.IsTrue(_log.Contains("FROM [sm].[Companies"));
    }

    [TestMethod]
    public void NoTrackingQueriesDoNotCacheObjects() {
      _activeStockRepository.All();
      Assert.AreEqual(0, _context.ChangeTracker.Entries().Count());
    }

    [TestMethod]
    public void CanQueryWithSinglePredicate() {
      _activeStockRepository.FindBy(c => c.PriceTraded > 700.00);
      WriteLog();
      Assert.IsTrue((_log.Contains("700")) && (_log.Contains(">")));
    }

    [TestMethod]
    public void CanQueryWithDualPredicate() {

    new GenericRepository<Company>(_context)
                
   .FindBy(c => c.CompanyName.StartsWith("L") && c.CompanyName.StartsWith("N"));
       WriteLog();
       Assert.IsTrue(_log.Contains("'L%'") && _log.Contains("'N%'"));
   }

    [TestMethod]
    public void CanIncludeNavigationProperties() {
      var results = new GenericRepository<Company>(_context).AllInclude(c => c.UserStocks);
      WriteLog();
      Assert.IsTrue(_log.Contains("UserStocks"));
      Assert.IsTrue(results.Any(c => c.UserStocks.Any()));
    }

    [TestMethod]
    public void ComposedOnAllListExecutedInMemory() {
      _activeStockRepository.All().Where(c => c.PriceTraded > 800).ToList();
      WriteLog();
      Assert.IsFalse(_log.Contains("> 800)"));
    }

    
    private void WriteLog() {
      Debug.WriteLine(_log);
    }

    private void SetupLogging() {
      _context.Database.Log = BuildLogString;
    }

    private void BuildLogString(string message) {
      _logBuilder.Append(message);
      _log = _logBuilder.ToString();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StockExchange.Auth.Interfaces;
using StockExchange.Domain;
using StockExchange.Domain.ViewModels;
using StockExchange.Repo;

namespace StockExchange.Web.Controllers
{
    [Authorize]
    public class UserStocksController : Controller
    {
        private GenericRepository<UserStock> _userStockRepository;
        private GenericRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public UserStocksController()
        {
        }

        public UserStocksController(GenericRepository<UserStock> userStockRepository, IMapper mapper, GenericRepository<Company> companyRepository)
        {
            _userStockRepository = userStockRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // GET: UserStocks/Create
        public ActionResult Create()
        {
            var userName = ClaimsPrincipal.Current.Identity.Name;
            var existingUserStocksComanyId = _userStockRepository
                                    .FindByInclude(c => c.UserName == userName, p=> p.Company)
                                    .ToList().Select(c => c.CompanyId);

            
            var availableCodeForUser = _companyRepository
                                          .FindByInclude(c => !existingUserStocksComanyId.Contains(c.Id));

            var vm = availableCodeForUser.ToList().Select(c => _mapper.Map<Company, UserStockViewModel>(c)).ToList();
            return View(vm);
        }

        // POST: UserStocks/Create
        [HttpPost]
        public ActionResult Create(List<UserStockViewModel> collection)
        {
            if (ModelState.IsValid && collection !=null)
            {
                var userName = ClaimsPrincipal.Current.Identity.Name;

                foreach (var item in collection.Where(c=>c.IsAdded))
                {
                    _userStockRepository.Insert(new UserStock{CompanyId = item.CompanyId, UserName = userName});
                }
            }
             return RedirectToAction("Edit");
        }

        // GET: UserStocks/Edit/5
        public ActionResult Edit()
        {
            var userName = ClaimsPrincipal.Current.Identity.Name;
            var userStock = _userStockRepository.FindByInclude(c => c.UserName == userName, p => p.Company);

            var vm = userStock.ToList().Select(c => _mapper.Map<UserStock, UserStockViewModel>(c)).ToList();
            return View(vm);
        }

        // POST: UserStocks/Edit/5
        [HttpPost]
        public ActionResult Edit(List<UserStockViewModel> collection)
        {
            if (ModelState.IsValid && collection != null)
            {
                var userName = ClaimsPrincipal.Current.Identity.Name;

                foreach (var item in collection.Where(c => c.IsAdded))
                {
                    _userStockRepository.Delete(item.Id);
                }
            }
            return RedirectToAction("Edit");
        }
    }
}

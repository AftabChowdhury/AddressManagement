using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressManagement_2.Models;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace AddressManagement_2.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressContext _context;

        public AddressController(AddressContext context)
        {
            _context = context;
        }

        // GET: Address
        public async Task<IActionResult> Index()
        {
            return View(await _context.Addresses.ToListAsync());
        }

        // GET: Address/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            //Get all countries in dropdown
            GetAllCountries();

            if (id == 0)
                return View(new Address());
            else
                return View(_context.Addresses.Find(id));
        }

        // POST: Address/AddOrEdit
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("AddressId,FirstName,LastName,Street,PostCode,City,State,Country,Email,PhoneNumber")] Address address)
        {
            if (ModelState.IsValid){
                if (address.AddressId == 0)
                {
                    if (!string.IsNullOrEmpty(address.Email))
                    {
                        if (!CheckDuolicateEmailAddMode(address.Email))
                        {
                            _context.Add(address);
                        }
                        else
                        {
                            ViewBag.Message = "Email is already exist";
                            GetAllCountries();
                            return View(address);
                        }
                    }
                    else
                    {
                        _context.Add(address);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(address.Email))
                    {
                        if (!CheckDuolicateEmailUpdateMode(address.Email, address.AddressId))
                        {
                            _context.Update(address);
                        }
                        else
                        {
                            ViewBag.Message = "Email is already exist";
                            GetAllCountries();
                            return View(address);
                        }
                    }
                    else
                    {
                        _context.Update(address);
                    }
                }                    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Check Duplicate Email in Add Mode
        private bool CheckDuolicateEmailAddMode(string email)
        {
            return _context.Addresses.Any(e => e.Email == email);
        }

        //Check Duplicate Email in Update Mode
        private bool CheckDuolicateEmailUpdateMode(string email, int id)
        {
            return _context.Addresses.Any(e => e.Email == email && e.AddressId != id);
        }

        public void GetAllCountries()
        {
            List<string> CountryList = new List<string>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }
            CountryList.Sort();
            ViewBag.CountryList = CountryList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleLeasingApp.Models;

namespace VehicleLeasingApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleLeasingContext _context;

        public VehiclesController(VehicleLeasingContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicleLeasingContext = _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier);

            var report = await vehicleLeasingContext
                .GroupBy(v => new {
                    v.Manufacturer,
                    SupplierName = v.Supplier.Name,
                    BranchName = v.Branch.Name,
                    ClientName = v.Client.Name
                })
                .Select(g => new VehicleReportViewModel
                {
                    Manufacturer = g.Key.Manufacturer,
                    SupplierName = g.Key.SupplierName,
                    BranchName = g.Key.BranchName,
                    ClientName = g.Key.ClientName,
                    Count = g.Count()
                })
                .ToListAsync();

            ViewBag.VehicleReport = report;
            return View(await vehicleLeasingContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id");
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Year,SupplierId,BranchId,ClientId,DriverId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", vehicle.SupplierId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", vehicle.SupplierId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Year,SupplierId,BranchId,ClientId,DriverId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Id", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "Id", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", vehicle.SupplierId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}

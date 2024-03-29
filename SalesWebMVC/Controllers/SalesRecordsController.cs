﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch(DateTime? min, DateTime? max)
        {
            if (!min.HasValue)
            {
                min = new DateTime(DateTime.Now.Year - 1, 1, 1);
            }
            if (!max.HasValue)
            {
                min = DateTime.Now;
            }

            ViewData["min"] = min.Value.ToString("yyyy/MM/dd");
            ViewData["max"] = max.Value.ToString("yyyy/MM/dd");
            var result = _salesRecordsService.FindByDate(min, max);
            return View(result);
        }
        public IActionResult GroupingSearch(DateTime? min, DateTime? max)
        {
            if (!min.HasValue)
            {
                min = new DateTime(DateTime.Now.Year - 1, 1, 1);
            }
            if (!max.HasValue)
            {
                max = DateTime.Now;
            }

            ViewData["min"] = min.Value.ToString("yyyy/MM/dd");
            ViewData["max"] = max.Value.ToString("yyyy/MM/dd");

            var result = _salesRecordsService.FindByDateGrouping(min, max);

            return View(result);
        }
    }
}
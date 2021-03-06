﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestAuthentification.Models;
using TestAuthentification.Services;
using TestAuthentification.ViewModels.Planning;

namespace TestAuthentification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly BookYourCarContext _context;
        private readonly AuthService _authService;

        private ILogger _logger;

        public PlanningController(BookYourCarContext context)
        {
            _context = context;
            _authService = new AuthService(context);
        }

        // GET: api/Planning/5
        [HttpGet("{date}")]
        public async Task<IActionResult> GetPlanning([FromRoute] DateTime date)
        {
            var token = GetToken();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TokenService.ValidateToken(token) || !TokenService.VerifDateExpiration(token)) return Unauthorized();

            try
            {
                DayOfWeek today = date.DayOfWeek;

                PlanningViewModel planningVM = new PlanningViewModel();

                PlanningService planningService = new PlanningService(_context);

                //Get first and last day of current week
                Tuple<DateTime, DateTime> weeksDay = GetFirstAndLastDaysOfCurrentWeek(date);
                Tuple<DateTime, DateTime> weeksCurrentDay = GetFirstAndLastDaysOfCurrentWeek(DateTime.Today);

                planningVM.StartWeek = weeksDay.Item1;
                planningVM.EndWeek = weeksDay.Item2;

                //Get count reservations which end or start in the current week
                planningVM.StartReservationCount = planningService.GetStartReservationCountThisWeek(weeksCurrentDay);
                planningVM.EndReservationCount = planningService.GetEndReservationCountThisWeek(weeksCurrentDay);

                //Get all vehicle count and used vehicle count for today
                planningVM.TotalVehiclesCount = planningService.GetCountTotalVehicles();
                planningVM.UsedVehiclesCount = planningService.GetUsedCarToday();

                //Get List of vehicule and with reservations for the calendar, on each line, if there is a reservation, display on tooltip the name of the driver
                planningVM.ListOfReservationsByVehicule = planningService.GetReservationsByVehicule(GetFirstAndLastDaysOfCurrentWeek(date));

                return Ok(planningVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Error", "Erreur durant la récupération du planning.");
                return BadRequest(ModelState);
            }       
        }

        private Tuple<DateTime, DateTime> GetFirstAndLastDaysOfCurrentWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;

            DateTime firstDay = date.AddDays(-(int)dayOfWeek + 1);
            DateTime lastDay = date.AddDays(7 - (int)dayOfWeek);

            return new Tuple<DateTime,DateTime>(firstDay, lastDay);
        }

        #region utilitaire Token
        private string GetToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            if (token.StartsWith("Bearer"))
            {
                var tab = token.Split(" ");
                token = tab[1];
            }

            return token;
        }


        #endregion
    }
}
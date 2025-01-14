﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Postieri.Data;
using Postieri.DTO;
using Postieri.Mappings;
using Postieri.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Postieri.Services
{
    public class BusinessIntegrationService : IBusinessIntegrationService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public BusinessIntegrationService(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public Order GetOrders(Guid id)
        {
            var ordersFromDb = _context.Orders.Where(n => n.OrderId == id).Include(w => w.Products).FirstOrDefault();
            var orders = _context.Orders.FindAsync(id);
            var orderToReturn = _mapper.Map<Order>(ordersFromDb);
            return orderToReturn;
        }
        public ActionResult<List<Order>> GetAllOrders()
        {
            var orders = _context.Orders.Include(x => x.Products).ToList();
            return orders;
        }

        public bool PostOrder(OrderDto order)
        {
            if (order == null)
            {
                return false;
            }

            var _order = new Order();
            _mapper.Map(order, _order);
            _context.Orders.Add(_order);
            _context.SaveChangesAsync();

            return true;
        }
        public bool SaveBusiness(BusinessDto request)
        {
            if (request == null)
            {
                return false;
            }

            var alreadyExist = _context.Businesses.Where(x => x.BusinessName == request.BusinessName & x.Email == request.Email).FirstOrDefault();
            if (alreadyExist != null)
            {
                return false;
            }

            var business = new Business();
            business.BusinessName = request.BusinessName;
            business.Email = request.Email;
            business.PhoneNumber = request.PhoneNumber;
            business.BusinessToken = CreateToken(business);

            _context.Businesses.Add(business);
            _context.SaveChangesAsync();
            return true;

        }
        private string CreateToken(Business b)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, b.BusinessName),
                new Claim(ClaimTypes.Email, b.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

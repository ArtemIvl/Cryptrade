﻿using System;
namespace JwtAuthenticationManager.Entity
{
	public class User
	{
		public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Models.UserModels
{
    public class LoginCredentialsDataModel
    {
        /// <summary>
        /// The unique Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The users username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The users email
        /// </summary>
        public string Email { get; set; }

        public bool Confirmed { get; set; }

        /// <summary>
        /// The users login token
        /// </summary>
        public string Token { get; set; }
    }
}

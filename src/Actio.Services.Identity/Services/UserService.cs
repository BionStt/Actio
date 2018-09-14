﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await userRepository.GetAsync(email, false);
            if (user != null)
            {
                throw new ActioException(ErrorCode.UserWithGivenEmailExist);
            }

            user = new User(email, name);
            user.SetPassword(password, encrypter);
            await userRepository.AddAsync(user);
        }

        public async Task Login(string email, string password)
        {
            var user = await userRepository.GetAsync(email, false);
            if (user == null)
            {
                throw new ActioException(ErrorCode.InvalidUsernameOrPassword);
            }

            if (!user.ValidatePassword(password, encrypter))
            {
                throw new ActioException(ErrorCode.InvalidUsernameOrPassword);
            }
        }
    }
}

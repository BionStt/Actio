﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Exceptions;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Activity()
        {
        }

        public Activity(Guid id, string name, string description, Guid userId, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException(ErrorCode.EmptyActivityName);
            }

            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}

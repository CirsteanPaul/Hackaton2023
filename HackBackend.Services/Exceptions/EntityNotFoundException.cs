using HackBackend.Services.Constants;
using System;

namespace HackBackend.Services.Exceptions
{
    public class EntityNotFoundException : BaseException
    {
        public int EntityId { get; }
        public Type EntityType { get; }

        public EntityNotFoundException(int entityId, Type entityType) : base(ErrorCodes.GenericEntityNotFound, GetDefaultMessage(entityId, entityType))
        {
            this.EntityId = entityId;
            this.EntityType = entityType;
        }

        public EntityNotFoundException(int entityId, Type entityType, string message) : base(ErrorCodes.GenericEntityNotFound, message) 
        {
            this.EntityId = entityId;
            this.EntityType = entityType;
        }

        public EntityNotFoundException(ErrorCodes code, int entityId, Type entityType) : base(code, GetDefaultMessage(entityId, entityType))
        {
            this.EntityId = entityId;
            this.EntityType = entityType;
        }

        public EntityNotFoundException(ErrorCodes code, int entityId, Type entityType, string message) : base(code, message)
        {
            this.EntityId = entityId;
            this.EntityType = entityType;
        }

        private static string GetDefaultMessage(int entityId, Type entityType)
        {
            return $"Entity {entityType.Name} not found for id {entityId}";
        }
    }
}

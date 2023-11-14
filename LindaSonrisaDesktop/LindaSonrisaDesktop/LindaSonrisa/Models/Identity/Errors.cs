using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models
{
    public class Errors : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError { Code = nameof(DefaultError), Description = "Ha ocurrido un error." };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Ha ocurrido un error, el objeto ya ha sido modificado." };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = nameof(PasswordMismatch), Description = "La contraseña es incorrecta." };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError { Code = nameof(InvalidToken), Description = "Ha ingresado un código inválido." };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Un usuario con ese nombre ya existe." };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = nameof(InvalidUserName), Description = $"El nombre de usuario '{userName}' es inválido." };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = nameof(InvalidEmail), Description = $"La dirección de email '{email}' es incorrecta." };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = nameof(DuplicateUserName), Description = $"El usuario con ese rut ({userName}) ya existe, por favor ingresa un usuario diferente." };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = nameof(DuplicateEmail), Description = $"La dirección de email ({email}) ya se encuentra registrada." };
        }

        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError { Code = nameof(InvalidRoleName), Description = $"El nombre del rol '{role}' es inválido." };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"El nombre del rol '{role}' ya existe." };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "El usuario ya tiene contraseña." };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "El bloqueo no está habilitado para este usuario." };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"El usuario ya es parte del rol '{role}'." };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError { Code = nameof(UserNotInRole), Description = $"El usuario no es parte del rol '{role}'." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = nameof(PasswordTooShort), Description = $"La contraseña debe tener un largo mínimo de {length} caracteres." };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "La contraseña debe incluir al menos un carácter alfanumérico." };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "La contraseña debe incluir al menos una dígito (0-9)." };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "La contraseña debe incluir al menos una letra minúscula (a-z)." };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "La contraseña debe incluir al menos una letra mayúsculas (A-Z)." };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return base.PasswordRequiresUniqueChars(uniqueChars);
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return base.RecoveryCodeRedemptionFailed();
        }
    }
}

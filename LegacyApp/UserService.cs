using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameCorrect(firstName) 
                || IsLastNameCorrect(lastName) 
                || IsEmailCorrect(email) 
                || CalculateAgeUsingBirthdate(dateOfBirth) < 21
                )
            {
                return false;
            }

            var client = GetUserByIdFromDataBase(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (IsClientVeryImportant(client))
            {
                user.HasCreditLimit = false;
            }
            else if (IsClientImportant(client))
            {
                SetCreditLimitForImportantClient(user);
            }
            else
            {
                user.HasCreditLimit = true;
                SetCreditLimitForCasualClient(user);
            }

            if (IsCreditLimitUnderLowerBarrier(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        private static bool IsCreditLimitUnderLowerBarrier(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
        private static void SetCreditLimitForCasualClient(User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }
        private static void SetCreditLimitForImportantClient(User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }
        }
        private static bool IsClientImportant(Client client)
        {
            return client.Type == "ImportantClient";
        }
        private static bool IsClientVeryImportant(Client client)
        {
            return client.Type == "VeryImportantClient";
        }
        private static Client GetUserByIdFromDataBase(int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            return client;
        }
        private static int CalculateAgeUsingBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age;
        }
        private static bool IsEmailCorrect(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }
        private static bool IsLastNameCorrect(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }
        private static bool IsFirstNameCorrect(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}

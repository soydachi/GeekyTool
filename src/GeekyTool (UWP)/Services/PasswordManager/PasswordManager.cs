using System;
using System.Collections.Generic;
using Windows.Security.Credentials;

namespace GeekyTool.Services.PasswordManager
{
    public class PasswordManager : IPasswordManager
    {
        private PasswordVault vault;

        public PasswordManager()
        {
            vault = new PasswordVault();
        }

        /// <summary>
        /// Adds a credential to the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="userName">The user name that must be present in the credentials.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/>, <paramref name="userName"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        public void Add(string resource, string userName, string password)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "The argument is null");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "The argument is null");

            vault.Add(new PasswordCredential(resource, userName, password));
        }

        /// <summary>
        /// Adds a credential to the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        public void Add(string resource, string password)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "The argument is null");

            vault.Add(new PasswordCredential(resource, resource, password));
        }

        /// <summary>
        /// Searches the Credential Locker for credentials matching the resource specified.
        /// </summary>
        /// <param name="resource"> When this method returns, contains an IVectorView of credential objects that match the search criteria.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> is <c>Null</c></exception>
        /// <returns>The resource to be searched for.</returns>
        public IReadOnlyList<PasswordCredential> FindAllByResource(string resource)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");

            return vault.FindAllByResource(resource);
        }

        /// <summary>Searches the Credential Locker for credentials that match the user name specified.</summary>
        /// <returns>When this method returns, contains an IVectorView of credential objects that match the search criteria.</returns>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="userName"/> is <c>Null</c></exception>
        /// <param name="userName">The user name to be searched for.</param>
        public IReadOnlyList<PasswordCredential> FindAllByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "The argument is null");

            return vault.FindAllByUserName(userName);
        }

        /// <summary>Reads a credential from the Credential Locker.</summary>
        /// <returns>The returned credential that contains all the data.</returns>
        /// <param name="resource">The resource for which the credential is used.</param>
        /// <param name="userName">The user name that must be present in the credential.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or <paramref name="userName"/> is <c>Null</c>
        /// </exception>
        public PasswordCredential Retrieve(string resource, string userName)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "The argument is null");

            return vault.Retrieve(resource, userName);
        }

        /// <summary>Reads a credential from the Credential Locker.</summary>
        /// <returns>The returned credential that contains all the data.</returns>
        /// <param name="resource">The resource for which the credential is used.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> is <c>Null</c></exception>
        public PasswordCredential Retrieve(string resource)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");

            return vault.Retrieve(resource, resource);
        }

        /// <summary>Retrieves all of the credentials stored in the Credential Locker.</summary>
        /// <returns>When this method returns, contains an IVectorView output of credential objects that match the search criteria. This output is a snapshot and not dynamic. If the results are used for updating or deleting credentials, those changes won't be reflected in the previous output.</returns>
        public IReadOnlyList<PasswordCredential> RetrieveAll()
        {
            return vault.RetrieveAll();
        }

        /// <summary>
        /// Removes a credential from the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="userName">The user name that must be present in the credentials.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/>, <paramref name="userName"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        public void Remove(string resource, string userName, string password)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName), "The argument is null");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "The argument is null");

            vault.Remove(new PasswordCredential(resource, userName, password));
        }

        /// <summary>
        /// Removes a credential from the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        public void Remove(string resource, string password)
        {
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource), "The argument is null");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "The argument is null");

            vault.Remove(new PasswordCredential(resource, resource, password));
        }

        /// <summary>
        /// Removes all credentials from the Credential Locker.
        /// </summary>
        public void RemoveAll()
        {
            foreach (var passwordCredential in vault.RetrieveAll())
            {
                vault.Remove(passwordCredential);
            }
        }
    }
}

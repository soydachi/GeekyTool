using System;
using System.Collections.Generic;
using Windows.Security.Credentials;
using GeekyTool.Annotations;


// ReSharper disable once CheckNamespace
namespace GeekyTool.Services
{
    public interface IPasswordManager
    {
        /// <summary>
        /// Adds a credential to the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="userName">The user name that must be present in the credentials.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/>, <paramref name="userName"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        bool Add([NotNull] string resource, [NotNull] string userName, [NotNull] string password);
        /// <summary>
        /// Adds a credential to the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        bool Add([NotNull] string resource, [NotNull] string password);
        /// <summary>
        /// Searches the Credential Locker for credentials matching the resource specified.
        /// </summary>
        /// <param name="resource"> When this method returns, contains an IVectorView of credential objects that match the search criteria.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> is <c>Null</c></exception>
        /// <returns>The resource to be searched for.</returns>
        IReadOnlyList<PasswordCredential> FindAllByResource([NotNull] string resource);
        /// <summary>Searches the Credential Locker for credentials that match the user name specified.</summary>
        /// <returns>When this method returns, contains an IVectorView of credential objects that match the search criteria.</returns>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="userName"/> is <c>Null</c></exception>
        /// <param name="userName">The user name to be searched for.</param>
        IReadOnlyList<PasswordCredential> FindAllByUserName([NotNull] string userName);
        /// <summary>Reads a credential from the Credential Locker.</summary>
        /// <returns>The returned credential that contains all the data.</returns>
        /// <param name="resource">The resource for which the credential is used.</param>
        /// <param name="userName">The user name that must be present in the credential.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or <paramref name="userName"/> is <c>Null</c>
        /// </exception>
        PasswordCredential Retrieve([NotNull] string resource, [NotNull] string userName);
        /// <summary>Reads a credential from the Credential Locker.</summary>
        /// <returns>The returned credential that contains all the data.</returns>
        /// <param name="resource">The resource for which the credential is used.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> is <c>Null</c></exception>
        PasswordCredential Retrieve([NotNull] string resource);
        /// <summary>Retrieves all of the credentials stored in the Credential Locker.</summary>
        /// <returns>When this method returns, contains an IVectorView output of credential objects that match the search criteria. This output is a snapshot and not dynamic. If the results are used for updating or deleting credentials, those changes won't be reflected in the previous output.</returns>
        IReadOnlyList<PasswordCredential> RetrieveAll();
        /// <summary>
        /// Removes a credential from the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="userName">The user name that must be present in the credentials.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/>, <paramref name="userName"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        bool Remove([NotNull] string resource, [NotNull] string userName, [NotNull] string password);
        /// <summary>
        /// Removes a credential from the Credential Locker.
        /// </summary>
        /// <param name="resource">The resource for which the credentials are used.</param>
        /// <param name="password">The password for the created credentials.</param>
        /// <exception cref="ArgumentNullException">Dispatched when <paramref name="resource"/> or
        /// <paramref name="password"/> is <c>Null</c>
        /// </exception>
        bool Remove([NotNull] string resource, [NotNull] string password);
        /// <summary>
        /// Removes all credentials from the Credential Locker.
        /// </summary>
        bool RemoveAll();
    }
}

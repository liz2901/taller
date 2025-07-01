using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string UserType { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string FirstLastName { get; set; }
    public string SecondLastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool AcceptedTermsAndCondition { get; set; }
    public DateTime AcceptedTermsAndConditionAt { get; set; }
    public Guid? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiresIn { get; set; }
    public Guid? PublicAccessTokenId { get; set; }
    
    private readonly List<ShoppingCart> _shoppingCarts;
    public IReadOnlyCollection<ShoppingCart> ShoppingCarts => _shoppingCarts;
    
    protected User()
    {
        Id = Guid.NewGuid();
        _shoppingCarts = new List<ShoppingCart>();
    }

    public User(string userName, string userType, string firstName, string secondName, string firstLastName,
        string secondLastName, string password, string email, bool status, 
        bool acceptedTermsAndCondition, DateTime acceptedTermsAndConditionAt) : this()
    {
        UserName = userName;
        UserType = userType;
        FirstName = firstName;
        SecondName = secondName;
        FirstLastName = firstLastName;
        SecondLastName = secondLastName;
        Password = password;
        Email = email;
        Status = status;
        AcceptedTermsAndCondition = acceptedTermsAndCondition;
        AcceptedTermsAndConditionAt = acceptedTermsAndConditionAt;
    }

    public User(string userName, string firstName, string secondName, string firstLastName, string secondLastName,
        string password, string email, bool status, DateTime createdAt, DateTime updatedAt,
        bool acceptedTermsAndCondition, DateTime acceptedTermsAndConditionAt, Guid? resetPasswordToken,
        DateTime? resetPasswordTokenExpiresIn)
    {
        UserName = userName;
        FirstName = firstName;
        SecondName = secondName;
        FirstLastName = firstLastName;
        SecondLastName = secondLastName;
        Password = password;
        Email = email;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        AcceptedTermsAndCondition = acceptedTermsAndCondition;
        AcceptedTermsAndConditionAt = acceptedTermsAndConditionAt;
        ResetPasswordToken = resetPasswordToken;
        ResetPasswordTokenExpiresIn = resetPasswordTokenExpiresIn;
    }
}
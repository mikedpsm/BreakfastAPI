using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Models;

public class Breakfast
{
  public const int MinNameLength = 3;
  public const int MaxNameLength = 50;
  public const int MinDescriptionLength = 50;
  public const int MaxDescriptionLength = 150;
  public const int MinSavoryLength = 3;
  public const int MaxSavoryLength = 50;
  public const int MinSweetLength = 3;
  public const int MaxSweetLength = 50;
  public const int MinSavoryAndSweetCantBeTheSame = 1;
  public const int MaxSavoryAndSweetCantBeTheSame = 5;


  public Guid Id { get; }
  public string Name { get; }
  public string Description { get; }
  public DateTime StartDateTime { get; }
  public DateTime EndDateTime { get; }
  public DateTime LastModifiedDateTime { get; }
  public List<string> Savory { get; }
  public List<string> Sweet { get; }

  private Breakfast(
    Guid id,
    string name,
    string description,
    DateTime startDateTime,
    DateTime endDateTime,
    DateTime lastModifiedDateTime,
    List<string> savory,
    List<string> sweet)
  {
    Id = id;
    Name = name;
    Description = description;
    StartDateTime = startDateTime;
    EndDateTime = endDateTime;
    LastModifiedDateTime = lastModifiedDateTime;
    Savory = savory;
    Sweet = sweet;
  }

  public static ErrorOr<Breakfast> Create(
    string name,
    string description,
    DateTime startDateTime,
    DateTime endDateTime,
    List<string> savory,
    List<string> sweet,
    Guid? id = null
  )
  {
    List<Error> errors = new();

    if (name.Length is < MinNameLength or > MaxNameLength)
      errors.Add(Errors.Breakfast.InvalidName);

    if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
      errors.Add(Errors.Breakfast.InvalidDescription);

    // if (startDateTime >= endDateTime)
    //   errors.Add(Errors.Breakfast.EndDateTimeNotAfterStartDateTime);

    // if (savory.Count == 0)
    //   errors.Add(Errors.Breakfast.Savory.IsRequired);

    // if (sweet.Count == 0)
    //   errors.Add(Errors.Breakfast.Sweet.IsRequired);

    // if (savory.Count > 5)
    //   errors.Add(Errors.Breakfast.Savory.TooMany);

    // if (sweet.Count > 5)
    //   errors.Add(Errors.Breakfast.Sweet.TooMany);

    // if (savory.Intersect(sweet).Any())
    //   errors.Add(Errors.Breakfast.SavoryAndSweetCantBeTheSame);

    if (errors.Count > 0)
      return errors;

    return new Breakfast(
      id ?? Guid.NewGuid(),
      name,
      description,
      startDateTime,
      endDateTime,
      DateTime.UtcNow,
      savory,
      sweet
    );
  }

  public static ErrorOr<Breakfast> From(CreateBreakfastRequest request)
  {
    return Create(
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      request.Savory,
      request.Sweet
    );
  }

  public static ErrorOr<Breakfast> From(Guid id, UpsertBreakfastRequest request)
  {
    return Create(
      request.Name,
      request.Description,
      request.StartDateTime,
      request.EndDateTime,
      request.Savory,
      request.Sweet,
      id);
  }
}




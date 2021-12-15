using System.ComponentModel;

namespace SimpleLibrary.Core.Enum;

public enum BookCreationResult
{
    ExistingBookWithTheName,
    NoAuthor,
    NoBookType,
    SaveChangesFault,
    Successful
}
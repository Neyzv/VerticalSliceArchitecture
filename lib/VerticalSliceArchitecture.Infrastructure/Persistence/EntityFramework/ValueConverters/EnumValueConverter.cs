using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.EntityFramework.ValueConverters;

/// <summary>
/// A value converter which aims to convert enum values as string.
/// </summary>
/// <typeparam name="TEnum">The type of the enum value.</typeparam>
public sealed class EnumValueConverter<TEnum>()
    : ValueConverter<TEnum, string>(static v => v.ToString(), static v => Enum.Parse<TEnum>(v))
    where TEnum : struct, Enum;
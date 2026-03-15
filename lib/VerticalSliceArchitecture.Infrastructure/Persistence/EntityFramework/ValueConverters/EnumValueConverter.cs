using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.EntityFramework.ValueConverters;

public sealed class EnumValueConverter<TEnum>()
    : ValueConverter<TEnum, string>(static v => v.ToString(), static v => Enum.Parse<TEnum>(v))
    where TEnum : struct, Enum;
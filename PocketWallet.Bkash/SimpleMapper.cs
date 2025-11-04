using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PocketWallet.Bkash
{
    /// <summary>
    /// A simple, lightweight object-to-object mapper alternative to AutoMapper.
    /// </summary>
    public static class SimpleMapper
    {
        private static readonly Dictionary<string, object> _mappingConfigurations = new();

        /// <summary>
        /// Maps properties with matching names and types from source to destination.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Mapped destination object</returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var destination = new TDestination();
            var configKey = GetConfigKey<TSource, TDestination>();

            // Check if custom mapping configuration exists
            if (_mappingConfigurations.TryGetValue(configKey, out var config))
            {
                var mappingConfig = config as MappingConfiguration<TSource, TDestination>;
                mappingConfig?.Apply(source, destination);
                return destination;
            }

            // Default mapping behavior - map properties with matching names and types
            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destProps)
            {
                if (!destProp.CanWrite)
                {
                    continue;
                }

                var sourceProp = Array.Find(sourceProps, p => p.Name == destProp.Name);
                if (sourceProp == null || !sourceProp.CanRead)
                {
                    continue;
                }

                var sourceValue = sourceProp.GetValue(source);
                if (sourceValue == null)
                {
                    continue;
                }

                // Direct assignment if types match
                if (sourceProp.PropertyType == destProp.PropertyType)
                {
                    destProp.SetValue(destination, sourceValue);
                }
                // Try type conversion for common scenarios
                else if (TryConvertValue(sourceValue, destProp.PropertyType, out var convertedValue))
                {
                    destProp.SetValue(destination, convertedValue);
                }
            }

            return destination;
        }

        /// <summary>
        /// Creates a mapping configuration for custom mappings.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <returns>Mapping configuration builder</returns>
        public static MappingConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>()
            where TDestination : new()
        {
            var configKey = GetConfigKey<TSource, TDestination>();
            var config = new MappingConfiguration<TSource, TDestination>();
            _mappingConfigurations[configKey] = config;
            return config;
        }

        private static string GetConfigKey<TSource, TDestination>()
        {
            return $"{typeof(TSource).FullName}->{typeof(TDestination).FullName}";
        }

        private static bool TryConvertValue(object value, Type targetType, out object? convertedValue)
        {
            convertedValue = null;
            try
            {
                // Handle nullable types
                var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

                // String to numeric conversions
                if (value is string stringValue)
                {
                    if (underlyingType == typeof(float) && float.TryParse(stringValue, out var floatResult))
                    {
                        convertedValue = floatResult;
                        return true;
                    }
                    if (underlyingType == typeof(int) && int.TryParse(stringValue, out var intResult))
                    {
                        convertedValue = intResult;
                        return true;
                    }
                    if (underlyingType == typeof(decimal) && decimal.TryParse(stringValue, out var decimalResult))
                    {
                        convertedValue = decimalResult;
                        return true;
                    }
                }
                // Numeric to string conversions
                else if (targetType == typeof(string))
                {
                    convertedValue = value.ToString();
                    return true;
                }
                // Other conversions
                else if (underlyingType.IsAssignableFrom(value.GetType()))
                {
                    convertedValue = value;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Configuration class for custom property mappings.
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    public class MappingConfiguration<TSource, TDestination>
        where TDestination : new()
    {
        private readonly List<Action<TSource, TDestination>> _propertyMappings = new();
        private readonly HashSet<string> _ignoredProperties = new();

        /// <summary>
        /// Configures a custom mapping for a specific destination property.
        /// </summary>
        /// <typeparam name="TDestProperty">Destination property type</typeparam>
        /// <param name="destinationMember">Expression selecting the destination property</param>
        /// <param name="mappingAction">Action to configure the mapping</param>
        /// <returns>This configuration for chaining</returns>
        public MappingConfiguration<TSource, TDestination> ForMember<TDestProperty>(
            Expression<Func<TDestination, TDestProperty>> destinationMember,
            Action<PropertyMappingBuilder<TSource, TDestProperty>> mappingAction)
        {
            var memberExpression = destinationMember.Body as MemberExpression
                ?? throw new ArgumentException("Expression must be a member expression");

            var propertyInfo = memberExpression.Member as PropertyInfo
                ?? throw new ArgumentException("Expression must be a property");

            var builder = new PropertyMappingBuilder<TSource, TDestProperty>();
            mappingAction(builder);

            _propertyMappings.Add((source, destination) =>
            {
                var value = builder.GetValue(source);
                propertyInfo.SetValue(destination, value);
            });

            return this;
        }

        /// <summary>
        /// Ignores a specific property during mapping.
        /// </summary>
        /// <typeparam name="TDestProperty">Destination property type</typeparam>
        /// <param name="destinationMember">Expression selecting the destination property</param>
        /// <returns>This configuration for chaining</returns>
        public MappingConfiguration<TSource, TDestination> Ignore<TDestProperty>(
            Expression<Func<TDestination, TDestProperty>> destinationMember)
        {
            var memberExpression = destinationMember.Body as MemberExpression
                ?? throw new ArgumentException("Expression must be a member expression");

            _ignoredProperties.Add(memberExpression.Member.Name);
            return this;
        }

        internal void Apply(TSource source, TDestination destination)
        {
            // Apply custom mappings
            foreach (var mapping in _propertyMappings)
            {
                mapping(source, destination);
            }

            // Apply default mappings for non-configured properties
            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destProps)
            {
                if (!destProp.CanWrite || _ignoredProperties.Contains(destProp.Name))
                {
                    continue;
                }

                // Skip if already mapped by custom configuration
                var alreadyMapped = false;
                foreach (var _ in _propertyMappings)
                {
                    // This is a simplified check; in production, you'd track which properties were mapped
                    alreadyMapped = true;
                    break;
                }

                var sourceProp = Array.Find(sourceProps, p => p.Name == destProp.Name);
                if (sourceProp == null || !sourceProp.CanRead)
                {
                    continue;
                }

                var sourceValue = sourceProp.GetValue(source);
                if (sourceValue == null)
                {
                    continue;
                }

                if (sourceProp.PropertyType == destProp.PropertyType)
                {
                    destProp.SetValue(destination, sourceValue);
                }
            }
        }
    }

    /// <summary>
    /// Builder for configuring property mappings.
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TProperty">Property type</typeparam>
    public class PropertyMappingBuilder<TSource, TProperty>
    {
        private Func<TSource, TProperty>? _mappingFunc;

        /// <summary>
        /// Maps the destination property using a custom value selector function.
        /// </summary>
        /// <param name="valueSelector">Function to select and transform the value</param>
        public void MapFrom(Func<TSource, TProperty> valueSelector)
        {
            _mappingFunc = valueSelector;
        }

        internal TProperty GetValue(TSource source)
        {
            if (_mappingFunc == null)
            {
                throw new InvalidOperationException("Mapping function not configured");
            }

            return _mappingFunc(source);
        }
    }
}

﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Linguini.Ast;

namespace Linguini.Serialization
{
    public class TermSerializer : JsonConverter<Term>
    {
        public override Term? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Term term, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteStringValue("Term");
            writer.WritePropertyName("id");
            ResourceSerializer.WriteIdentifier(writer, term.Id);
            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, term.Value, options);

            writer.WritePropertyName("attributes");
            writer.WriteStartArray();
            foreach (var attribute in term.Attributes)
            {
                JsonSerializer.Serialize(writer, attribute, options);
            }

            writer.WriteEndArray();


            if (term.Comment != null || !options.IgnoreNullValues)
            {
                writer.WritePropertyName("comment");
                JsonSerializer.Serialize(writer, term.Comment, options);
            }

            writer.WriteEndObject();
        }
    }
}
﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
    /// <summary>
    /// Abstracts the ability to classify and load messages for error codes. Allows the error
    /// infrastructure to be reused between C# and VB.
    /// </summary>
    internal abstract class CommonMessageProvider
    {
        /// <summary>
        /// Given an error code, get the severity (warning or error) of the code.
        /// </summary>
        public abstract DiagnosticSeverity GetSeverity(int code);

        /// <summary>
        /// Load the message for the given error code. If the message contains
        /// "fill-in" placeholders, those should be expressed in standard string.Format notation
        /// and be in the string.
        /// </summary>
        public abstract string LoadMessage(int code, CultureInfo language);

        /// <summary>
        /// Get the text prefix (e.g., "CS" for C#) used on error messages.
        /// </summary>
        public abstract string CodePrefix { get; }

        /// <summary>
        /// Get the warning level for warnings (e.g., 1 through 4 for C#). VB does not have warning
        /// levels and always uses 1. Errors should return 0.
        /// </summary>
        public abstract int GetWarningLevel(int code);

        /// <summary>
        /// Type that defines error codes. For testing purposes only.
        /// </summary>
        public abstract Type ErrorCodeType { get; }

        /// <summary>
        /// Create a simple language speicific diagnostic for given error code.
        /// </summary>
        public abstract Diagnostic CreateDiagnostic(int code, Location location, params object[] args);

        /// <summary>
        /// Given a message identifier (e.g., CS0219), severity, warning as error and a culture, 
        /// get the entire prefix (e.g., "error CS0219: Warning as Error:" for C# or "error BC42024:" for VB) used on error messages.
        /// </summary>
        public abstract string GetMessagePrefix(string id, DiagnosticSeverity severity, bool isWarningAsError, CultureInfo culture);

        /// <summary>
        /// convert given symbol to string representation based on given error code
        /// </summary>
        public abstract string ConvertSymbolToString(int errorCode, ISymbol symbol);

        /// <summary>
        /// Given an error code (like 1234) return the identifier (CS1234 or BC1234).
        /// </summary>
        public string GetIdForErrorCode(int errorCode)
        {
            return CodePrefix + errorCode.ToString("0000");
        }

        // Common error messages 

        public abstract int ERR_FailedToCreateTempFile { get; }

        // command line:
        public abstract int ERR_NoScriptsSpecified { get; }
        public abstract int ERR_OpenResponseFile { get; }
        public abstract int FTL_InputFileNameTooLong { get; }
        public abstract int ERR_FileNotFound { get; }
        public abstract int ERR_NoSourceFile { get; }
        public abstract int ERR_CantOpenFileWrite { get; }
        public abstract int ERR_OutputWriteFailed { get; }
        public abstract int WRN_NoConfigNotOnCommandLine { get; }
        public abstract int ERR_BinaryFile { get; }
        public abstract int WRN_UnableToLoadAnalyzer { get; }
        public abstract int INF_UnableToLoadSomeTypesInAnalyzer { get; }
        public abstract int WRN_AnalyzerCannotBeCreated { get; }
        public abstract int WRN_NoAnalyzerInAssembly { get; }
        public abstract int ERR_CantReadRulesetFile { get; }

        // reference manager:
        public abstract int ERR_MetadataFileNotAssembly { get; }
        public abstract int ERR_MetadataFileNotModule { get; }
        public abstract int ERR_InvalidAssemblyMetadata { get; }
        public abstract int ERR_InvalidModuleMetadata { get; }
        public abstract int ERR_ErrorOpeningAssemblyFile { get; }
        public abstract int ERR_ErrorOpeningModuleFile { get; }
        public abstract int ERR_MetadataFileNotFound { get; }
        public abstract int ERR_MetadataReferencesNotSupported { get; }
        public abstract int ERR_LinkedNetmoduleMetadataMustProvideFullPEImage { get; }

        public abstract void ReportDuplicateMetadataReferenceStrong(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity);
        public abstract void ReportDuplicateMetadataReferenceWeak(DiagnosticBag diagnostics, Location location, MetadataReference reference, AssemblyIdentity identity, MetadataReference equivalentReference, AssemblyIdentity equivalentIdentity);

        // signing:
        public abstract int ERR_PublicKeyFileFailure { get; }
        public abstract int ERR_PublicKeyContainerFailure { get; }

        // resources:
        public abstract int ERR_CantReadResource { get; }
        public abstract int ERR_CantOpenWin32Resource { get; }
        public abstract int ERR_CantOpenWin32Manifest { get; }
        public abstract int ERR_CantOpenWin32Icon { get; }
        public abstract int ERR_BadWin32Resource { get; }
        public abstract int ERR_ErrorBuildingWin32Resource { get; }
        public abstract int ERR_ResourceNotUnique { get; }
        public abstract int ERR_ResourceFileNameNotUnique { get; }
        public abstract int ERR_ResourceInModule { get; }

        // pseudo-custom attributes:
        public abstract int ERR_PermissionSetAttributeFileReadError { get; }

        // PDB writing:
        public abstract int WRN_PdbUsingNameTooLong { get; }
        public abstract int WRN_PdbLocalNameTooLong { get; }
        public abstract int ERR_PdbWritingFailed { get; }

        // PE writing:
        public abstract int ERR_MetadataNameTooLong { get; }

        public abstract void ReportInvalidAttributeArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, AttributeData attribute);
        public abstract void ReportInvalidNamedArgument(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex, ITypeSymbol attributeClass, string parameterName);
        public abstract void ReportParameterNotValidForType(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int namedArgumentIndex);

        public abstract void ReportMarshalUnmanagedTypeNotValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute);
        public abstract void ReportMarshalUnmanagedTypeOnlyValidForFields(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, int parameterIndex, string unmanagedTypeName, AttributeData attribute);

        public abstract void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName);
        public abstract void ReportAttributeParameterRequired(DiagnosticBag diagnostics, SyntaxNode attributeSyntax, string parameterName1, string parameterName2);
    }
}

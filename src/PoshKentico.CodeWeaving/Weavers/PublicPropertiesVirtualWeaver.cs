// <copyright file="PublicPropertiesVirtualWeaver.cs" company="Chris Crutchfield">
// Copyright (C) 2017  Chris Crutchfield
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see &lt;http://www.gnu.org/licenses/&gt;.
// </copyright>

using Mono.Cecil;

namespace PoshKentico.CodeWeaving.Weavers
{
    /// <summary>
    /// Weaver wich makes public properties virtual.
    /// </summary>
    internal class PublicPropertiesVirtualWeaver : IWeaver
    {
        #region Methods

        /// <inheritdoc/>
        public void Execute(AssemblyDefinition assemblyDefinition)
        {
            foreach (var type in assemblyDefinition.MainModule.Types)
            {
                this.MakePropertiesVirtual(type);
            }
        }

        private void MakePropertiesVirtual(TypeDefinition typeDefinition)
        {
            foreach (var prop in typeDefinition.Properties)
            {
                this.MakePropertyMethodVirtual(prop.GetMethod);
                this.MakePropertyMethodVirtual(prop.SetMethod);
            }
        }

        private void MakePropertyMethodVirtual(MethodDefinition methodDefinition)
        {
            if (methodDefinition == null ||
                methodDefinition.IsStatic ||
                !methodDefinition.IsPublic ||
                methodDefinition.DeclaringType.IsInterface ||
                methodDefinition.IsAbstract ||
                (methodDefinition.IsVirtual & !methodDefinition.IsNewSlot))
            {
                return;
            }

            methodDefinition.Attributes =
                MethodAttributes.Public |
                MethodAttributes.HideBySig |
                MethodAttributes.SpecialName |
                MethodAttributes.NewSlot |
                MethodAttributes.Virtual;
        }

        #endregion

    }
}

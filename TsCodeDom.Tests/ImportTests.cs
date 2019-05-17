using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TsCodeDom.Tests
{
    [TestClass]
    public class ImportTests
    {
        /// <summary>
        /// Create imports
        /// </summary>
        [TestMethod]
        public void CreateImports()
        {
            TypeScriptProvider tsProvider = new TypeScriptProvider();

            TsCodeDom.Entities.TsCodeNamespace newNamespace = new Entities.TsCodeNamespace();
            // we don't want to use a namespace for the generated interface (so we are not setting the namespace name)
            //add import all (alternative 1) (simply not setting an import type)
            newNamespace.Imports.Add(new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package"
            });

            //add import all (alternative 2)
            var importWithTypes = new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package"
            };
            importWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                //dont set the name to import all
            });
            newNamespace.Imports.Add(importWithTypes);

            //add import all as
            importWithTypes = new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package"
            };
            importWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                //dont set the name to import all
                Alias = "myImport"
            });
            newNamespace.Imports.Add(importWithTypes);

            //add import with specific types
            importWithTypes = new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package"
            };
            importWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                Name = "Cat"
            });
            importWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                Name = "Dog",
                Alias = "Wuffi"
            });
            newNamespace.Imports.Add(importWithTypes);
            
            var tsCode = tsProvider.GenerateCodeFromNamespace(newNamespace);
            Assert.AreEqual(tsCode, "import * from '@scope/package';\r\nimport * from '@scope/package';\r\nimport * as myImport from '@scope/package';\r\nimport {Cat, Dog as Wuffi} from '@scope/package';\r\n");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsCodeDom.Tests
{
    [TestClass]
    public class ExportTests
    {
        /// <summary>
        /// Create Exports
        /// </summary>
        [TestMethod]
        public void CreateExports()
        {
            TypeScriptProvider tsProvider = new TypeScriptProvider();
            Entities.TsCodeNamespace newNamespace = new Entities.TsCodeNamespace();
            //add export all (alternative 1) (simply not setting an import type)
            newNamespace.Imports.Add(new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package",
                IsExport = true
            });

            //add export all (alternative 2)
            var exportWithTypes = new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package",
                IsExport = true
            };
            exportWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                //dont set the name to import all
            });
            newNamespace.Imports.Add(exportWithTypes);

            //export multiple types
            exportWithTypes = new Entities.TsCodeNamespaceImport()
            {
                Path = "@scope/package",
                IsExport = true
            };
            exportWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                Name = "Dog"
            });
            exportWithTypes.ImportTypes.Add(new Entities.TsCodeImportType()
            {
                Name = "Cat"
            });
            newNamespace.Imports.Add(exportWithTypes);

            var tsCode = tsProvider.GenerateCodeFromNamespace(newNamespace);
            Assert.AreEqual(tsCode, "export * from '@scope/package';\r\nexport * from '@scope/package';\r\nexport {Cat, Dog} from '@scope/package';\r\n");
        }
    }
}

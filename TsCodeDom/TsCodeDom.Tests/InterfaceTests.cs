using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Tests
{
    [TestClass]
    public class InterfaceTests
    {
        /// <summary>
        /// Create interface
        /// </summary>
        [TestMethod]
        public void CreateInterface()
        {
            TypeScriptProvider tsProvider = new TypeScriptProvider();
            TsCodeDom.Entities.TsCodeNamespace newNamespace = new Entities.TsCodeNamespace();
            
            // create interface
            TsCodeDom.Entities.TsCodeTypeDeclaration interfaceType = new Entities.TsCodeTypeDeclaration("myInterface");            
            interfaceType.ElementType = TsElementTypes.Interface;
            
            // add base type
            interfaceType.BaseTypes.Add(new Entities.TsCodeTypeReference("IBase"));
            
            //add a comment
            interfaceType.Comments.Add(new Entities.TsCodeCommentStatement("This is myInterface description"));            
            
            // we want to export the interface so we set the public attribute
            interfaceType.Attributes = TsTypeAttributes.Public;

            // setting a decorator
            interfaceType.Decorators.Add(new Entities.TsCodeAttributeDeclaration()
            {
                Name = "MyDecorator"
            });

            //add field members
            var field = new TsCodeDom.Entities.TsCodeMemberField()
            {
                Name = "myField"
            };
            field.Comments.Add(new Entities.TsCodeCommentStatement("This is my property"));

            // field can have multiple types
            field.Types.Add(new Entities.TsCodeTypeReference(typeof(string)));
            field.Types.Add(new Entities.TsCodeTypeReference(typeof(int)));
            field.Types.Add(new Entities.TsCodeTypeReference("Cat"));            
            interfaceType.Members.Add(field);

            // method
            var method = new Entities.TsCodeMemberMethod() {
                Name = "myMethod"             
            };
            method.Comments.Add(new Entities.TsCodeCommentStatement("This is my method"));

            // add method parameter
            var methodParameter = new Entities.TsCodeParameterDeclarationExpression("myParam")
            {
                IsNullable = true
            };            
            methodParameter.Types.Add(new Entities.TsCodeTypeReference(typeof(DateTime)));
            method.Parameters.Add(methodParameter);

            method.ReturnType = new Entities.TsCodeTypeReference(typeof(string));
            interfaceType.Members.Add(method);


            newNamespace.Types.Add(interfaceType);
            var tsCode = tsProvider.GenerateCodeFromNamespace(newNamespace);

            Assert.AreEqual(tsCode, "\r\n/** This is myInterface description */\r\n@MyDecorator()\r\nexport interface myInterface extends IBase {\r\n    /** This is my property */\r\n    myField: string | number | Cat;\r\n\r\n    /** This is my method */\r\n    myMethod(myParam?: Date): string;\r\n}\r\n");
        }
    }
}

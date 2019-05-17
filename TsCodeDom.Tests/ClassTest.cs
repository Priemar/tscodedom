using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Entities;

namespace TsCodeDom.Tests
{
    [TestClass]
    public class ClassTest
    {
        /// <summary>
        /// Create class with property
        /// </summary>
        [TestMethod]
        public void CreateClass_WithProperty()
        {
            TypeScriptProvider tsProvider = new TypeScriptProvider();
            TsCodeDom.Entities.TsCodeNamespace newNamespace = new Entities.TsCodeNamespace();

            // create a public abstract class
            var classType = new Entities.TsCodeTypeDeclaration("MyClass")
            {
                ElementType = Enumerations.TsElementTypes.Class,
                Attributes = Enumerations.TsTypeAttributes.Abstract | Enumerations.TsTypeAttributes.Public
            };

            // add comment
            classType.Comments.Add(new Entities.TsCodeCommentStatement("This is my class"));

            // add decorator
            classType.Decorators.Add(new Entities.TsCodeAttributeDeclaration()
            {
                Name = "MyDecorator"
            });            

            // inherit from class
            classType.BaseTypes.Add(new Entities.TsCodeTypeReference("MyBaseClass"));
            // implement interface
            classType.BaseTypes.Add(new Entities.TsCodeTypeReference("IBase", Enumerations.TsElementTypes.Interface));


            var myPropertyField = new Entities.TsCodeMemberField()
            {
                Name = "_myProperty",
                //Attributes = Enumerations.TsMemberAttributes.Private,
                TypeAttributes = Enumerations.TsTypeAttributes.Private
            };
            myPropertyField.Types.Add(new Entities.TsCodeTypeReference(typeof(string)));
            myPropertyField.InitStatement = new Entities.TsCodePrimitiveExpression("init value");
            classType.Members.Add(myPropertyField);


            //add property
            var myProperty = new Entities.TsCodeMemberProperty("myProperty");
            myProperty.Types.Add(new Entities.TsCodeTypeReference(typeof(string)));
            myProperty.Attributes = Enumerations.TsMemberAttributes.Protected;
            myProperty.TypeAttributes = Enumerations.TsTypeAttributes.Readonly;
            myProperty.SetParameterName = "value";            
            myProperty.HasGet = true;
            myProperty.HasSet = true;
            
            //helper: reference to this.myField (comine fieldname with this operator)
            var thisFieldExpression = new TsCodeFieldReferenceExpression()
            {
                FieldName = myPropertyField.Name,
                TargetObject = new TsCodeThisReferenceExpression()
            };

            //define getter return statements
            var getterReturnStatement = new TsCodeMethodReturnStatement()
            {
                Expression = thisFieldExpression
            };
            myProperty.GetStatements.Add(getterReturnStatement);
            
            //define setter statement
            var setterStatement = new TsCodeAssignStatement();
            setterStatement.Left = new TsCodeVariableReferenceExpression(myPropertyField.Name);
            setterStatement.Right = new TsCodeVariableReferenceExpression(myProperty.SetParameterName);

            myProperty.SetStatements.Add(setterStatement);

            classType.Members.Add(myProperty);


            newNamespace.Types.Add(classType);
            var tsCode = tsProvider.GenerateCodeFromNamespace(newNamespace);

            Assert.AreEqual(tsCode, "\r\n/** This is my class */\r\n@MyDecorator()\r\nexport abstract class MyClass extends MyBaseClass implements IBase {\r\n    private _myProperty: string = 'init value';\r\n    protected get myProperty(): string{\r\n        return this._myProperty;\r\n    }\r\n    protected set myProperty(value: string){\r\n        _myProperty = value;\r\n    }\r\n}\r\n");
        }

        /// <summary>
        /// Create class with method
        /// </summary>
        [TestMethod]
        public void CreateClass_WithMethod()
        {
            TypeScriptProvider tsProvider = new TypeScriptProvider();
            TsCodeDom.Entities.TsCodeNamespace newNamespace = new Entities.TsCodeNamespace();

            // create a public abstract class
            var classType = new Entities.TsCodeTypeDeclaration("MyClass")
            {
                ElementType = Enumerations.TsElementTypes.Class,
                Attributes = Enumerations.TsTypeAttributes.Public
            };
            
            var myMethod = new TsCodeMemberMethod()
            {
                Name ="myMethod",
                Attributes = Enumerations.TsMemberAttributes.Private,
                // type void
                ReturnType = new TsCodeTypeReference()
            };

            // add console statement
            var consoleLogExpression = new TsCodeMethodInvokeExpression()
            {
                Method = new TsCodeMethodReferenceExpression()
                {
                    MethodName = "log",
                    TargetObject = new TsCodeVariableReferenceExpression("console")
                }
            };
            consoleLogExpression.Parameters.Add(new TsCodePrimitiveExpression("hello world"));
            myMethod.Statements.Add(new TsCodeExpressionStatement(consoleLogExpression));
            classType.Members.Add(myMethod);

            newNamespace.Types.Add(classType);
            var tsCode = tsProvider.GenerateCodeFromNamespace(newNamespace);
        }
    }
}

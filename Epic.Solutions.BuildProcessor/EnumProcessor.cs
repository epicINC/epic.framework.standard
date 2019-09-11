using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace Epic.Solutions.BuildProcessor
{
    public class EnumProcessor : IProcessor
    {
        // Epic.Solutions.Components.IEnumConstraint
        public void Process(ModuleDefinition module)
        {

            this.EnumType = module.GetType("Epic.Enums.EnumInternal`1").Methods.SingleOrDefault(e => e.Name == "Mark").ReturnType;


            this.IEnumConstraintType = module.GetType("Epic.Components.IEnumConstraint");
            Console.WriteLine("Current Module: {0}", module.FileName);


            var types = module.Types.Where(e => e.IsClass || e.IsInterface);


            this.ChangeTypes(types);

            // Class
            //ChangeConstraints(types.SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k != null && k.FullName == "Epic.Components.IEnumConstraint")));

            // Methods Constraints
            //ChangeConstraints(types.SelectMany(e => e.Methods).SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k != null && k.FullName == "Epic.Components.IEnumConstraint")));
        }


        void ChangeTypes(IEnumerable<TypeDefinition> collection)
        {
            if (collection == null || !collection.Any()) return;
            foreach (var item in collection)
                ChangeType(item);
        }

        void ChangeType(TypeDefinition value)
        {
            if (value.HasGenericParameters) this.ChangeConstraints(value.GenericParameters.Select(e => e.Constraints));
            this.ChangeConstraints(value.Methods.SelectMany(e => e.GenericParameters).Select(e => e.Constraints));
            this.ChangeTypes(value.NestedTypes);
        }

        void ChangeConstraints(IEnumerable<Collection<GenericParameterConstraint>> collection)
        {
            if (collection == null || !collection.Any()) return;
            
            foreach (var item in collection)
                ChangeConstraints(item);
        }
        
        void ChangeConstraints(Collection<GenericParameterConstraint> collection)
        {
            var item = collection.FirstOrDefault(e => e.ConstraintType == this.IEnumConstraintType);
            if (item == null) return;

            collection.Remove(item);
            collection.Add(new GenericParameterConstraint(this.EnumType));

            if (this.ValueType == null) this.ValueType = collection.FirstOrDefault(e => e.ConstraintType.FullName == "System.ValueType");
            if (this.ValueType == null) return;
            collection.Remove(this.ValueType);
        }

        TypeReference IEnumConstraintType
        {
            get;
            set;
        }

        GenericParameterConstraint ValueType
        {
            get;
            set;
        }

        TypeReference EnumType
        {
            get;
            set;
        }
    }

}

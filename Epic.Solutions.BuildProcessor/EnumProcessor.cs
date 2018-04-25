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
            this.EnumType = module.Import(typeof(System.Enum));

            var types = module.Types.Where(e => e.IsClass || e.IsInterface);

            ChangeConstraints(types.SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k.Name == "IEnumConstraint")));
            ChangeConstraints(types.SelectMany(e => e.Methods).SelectMany(e => e.GenericParameters).Select(e => e.Constraints).Where(e => e.Any(k => k.Name == "IEnumConstraint")));
        }

        void ChangeConstraints(IEnumerable<Collection<TypeReference>> collection)
        {
            foreach (var item in collection)
            {
                ChangeConstraints(item);
            }
        }

        void ChangeConstraints(Collection<TypeReference> collection)
        {
            if (this.IEnumConstraintType == null)
                this.IEnumConstraintType = collection.Single(e => e.Name == "IEnumConstraint");
            collection.Remove(this.IEnumConstraintType);
            collection.Add(this.EnumType);
        }

        TypeReference IEnumConstraintType
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

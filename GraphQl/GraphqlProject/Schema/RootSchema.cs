using GraphqlProject.Mutation;
using GraphqlProject.Query;

namespace GraphqlProject.Schema
{
    public class RootSchema : GraphQL.Types.Schema
    {
        
        public RootSchema(IServiceProvider services) : base(services) 
        {
            Query = services.GetRequiredService<RootQuery>();
            Mutation = services.GetRequiredService<RootMutation>();
        }
        
       /* public RootSchema(RootQuery rootQuery, RootMutation rootMutation )  
        {
            this.Query = rootQuery;
            this.Mutation = rootMutation;
        }*/
    }
}

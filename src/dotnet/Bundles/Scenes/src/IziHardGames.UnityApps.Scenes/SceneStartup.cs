using UnityEngine;

namespace IziHardGames.UnityScenes
{
    public class SceneStartup : MonoBehaviour
    {
        //[SerializeField] private ScriptableApp? scriptableApp;
        //private bool isAwaitedScriptableApp;
        //private bool isDependeciesResolved;
        //public List<MonoBehaviour> components;

        //private void Awake()
        //{
        //    isAwaitedScriptableApp = false;
        //    isDependeciesResolved = false;
        //}

        //private void Update()
        //{
        //    if (!isAwaitedScriptableApp)
        //    {
        //        if (scriptableApp?.AwaitInitilization().Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
        //        {
        //            BeginSceneInitilization();
        //            isAwaitedScriptableApp = true;
        //        }
        //        else return;
        //    }
        //    else
        //    {
        //        if (InjuectServices())
        //        {
        //            isDependeciesResolved = true;
        //            FinishInitilization();
        //        }
        //    }
        //}

        //private void Reset()
        //{
        //    Validate();
        //}

        //private void Validate()
        //{
        //    enabled = true;
        //    components = IziFind.FindComponentsAllAtScene<MonoBehaviour>(gameObject.scene).ToList();
        //}


        //private void FinishInitilization()
        //{
        //    this.enabled = false;
        //    scriptableApp.CompleteCallback(gameObject.scene.buildIndex);
        //}

        //protected virtual void BeginSceneInitilization()
        //{
        //}

        //private bool InjuectServices()
        //{
        //    foreach (var component in components)
        //    {
        //        InjuectServices(component);
        //    }
        //    return true;
        //}

        //private bool InjuectServices(Component component)
        //{
        //    var props = component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        //    var provider = scriptableApp.ServiceProvider;
        //    var type = component.GetType();
        //    if (type.Name == "SpwanerSample")
        //    {
        //        Debug.Log("Spawner");
        //    }
        //    foreach (var prop in props)
        //    {
        //        var atrInject = prop.GetCustomAttributes().FirstOrDefault(x => x is IziInjectAttribute) as IziInjectAttribute;
        //        if (atrInject != null)
        //        {
        //            if (prop.CanWrite)
        //            {
        //                if (atrInject.InjectType is null)
        //                {
        //                    prop.SetValue(component, provider.GetService(prop.PropertyType) ?? throw new NullReferenceException($"Field: [{prop.PropertyType.AssemblyQualifiedName}.{prop.Name}] as [{prop.PropertyType}]"));
        //                }
        //                else
        //                {
        //                    prop.SetValue(component, provider.GetService(atrInject.InjectType) ?? throw new NullReferenceException($"Field: [{prop.PropertyType.AssemblyQualifiedName}.{prop.Name}] as [{atrInject.InjectType.AssemblyQualifiedName}]"));
        //                }
        //            }
        //            else
        //            {
        //                var backingFieldName = $"<{prop.Name}>k__BackingField";
        //                FieldInfo fieldInfo = type.GetField(backingFieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        //                if (fieldInfo != null)
        //                {
        //                    if (atrInject.InjectType is null)
        //                    {
        //                        fieldInfo.SetValue(component, provider.GetService(fieldInfo.FieldType) ?? throw new NullReferenceException($"Field: [{fieldInfo.FieldType.AssemblyQualifiedName}.{fieldInfo.Name}] as [{fieldInfo.FieldType.AssemblyQualifiedName}]"));
        //                    }
        //                    else
        //                    {
        //                        fieldInfo.SetValue(component, provider.GetService(atrInject.InjectType) ?? throw new NullReferenceException($"Field: [{fieldInfo.FieldType.AssemblyQualifiedName}.{fieldInfo.Name}] as [{atrInject.InjectType.AssemblyQualifiedName}]"));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    var fields = component.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        //    foreach (var field in fields)
        //    {
        //        var atrInject = field.GetCustomAttributes().FirstOrDefault(x => x is IziInjectAttribute) as IziInjectAttribute;
        //        if (atrInject != null)
        //        {
        //            if (atrInject.InjectType is null)
        //            {
        //                field.SetValue(component, provider.GetService(field.FieldType) ?? throw new NullReferenceException($"Field: [{field.FieldType.AssemblyQualifiedName}.{field.Name}] as [{field.FieldType.AssemblyQualifiedName}]"));
        //            }
        //            else
        //            {
        //                field.SetValue(component, provider.GetService(atrInject.InjectType) ?? throw new NullReferenceException($"Field: [{field.FieldType.AssemblyQualifiedName}.{field.Name}] as [{atrInject.InjectType.AssemblyQualifiedName}]"));
        //            }
        //        }
        //    }
        //    return true;
        //}
    }
}

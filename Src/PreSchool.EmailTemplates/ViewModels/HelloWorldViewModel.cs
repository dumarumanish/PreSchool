namespace PreSchool.EmailTemplates.ViewModels
{
  public class HelloWorldViewModel
  {
    public string ButtonLink { get; set; }
    
    public HelloWorldViewModel(string buttonLink)
    {
      ButtonLink = buttonLink;
    }
  }
}
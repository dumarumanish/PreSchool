namespace PreSchool.Data.Entities.Seo
{
    /// <summary>
    /// Represents an entity which supports slug (SEO friendly one-word URLs)
    /// </summary>
    public interface ISlugSupported
    {
        int? SeoUrlId { get; set; }
        SeoUrl SeoUrl { get; set; }

    }
}

namespace Lunox.Library.Enum
{
    #region ImageTypeEnum

    /// <summary>
    /// 
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// A JPG file is a raster image saved in the JPEG format, commonly used to store digital
        /// photographs and graphics created by image-editing software. JPEG features lossy compression
        /// that can significantly reduce the size of an image without much degradation and supports up
        /// to 16,777,216 colors.
        /// </summary>
        JPG,
        /// <summary>
        /// A JPEG file is an image saved in a compressed graphic format standardized by the Joint
        /// Photographic Experts Group (JPEG). It supports up to 24-bit color and is compressed using
        /// lossy compression, which may noticeably reduce the image quality if high amounts of compression
        /// are used. JPEG files are commonly used for storing digital photos and web graphics.
        /// </summary>
        JPEG,
        /// <summary>
        /// A PNG file is an image saved in the Portable Network Graphic (PNG) format, commonly used to
        /// store web graphics, digital photographs, and images with transparent backgrounds. It is a
        /// raster graphic similar to a .JPG image but is compressed with lossless compression and supports
        /// transparency.
        /// </summary>
        PNG,
        /// <summary>
        /// An SVG file is a graphic saved in a two-dimensional vector graphic format created by the World
        /// Wide Web Consortium (W3C). It stores information that describes an image in a text format that
        /// is based on XML. SVG files may include vector shapes, embedded raster graphics (also known as
        /// bitmap images), and text.
        /// </summary>
        SVG,
        /// <summary>
        /// A BMP file is an image saved in the Bitmap (BMP) raster image format developed by Microsoft.
        /// It contains uncompressed image data that supports monochrome and color images at variable color
        /// bit depths, along with image metadata. BMP files are commonly used for storing 2D digital photos.
        /// </summary>
        BMP,
        /// <summary>
        /// Image stored in the JPEG XR format, which was originally developed by Microsoft for Windows
        /// Media software; supports deep color images with 48-bit RGB; used for high-resolution professional
        /// image editing; also supports lossless and lossy compression.
        /// </summary>
        JXR,
        /// <summary>
        /// A TIF file contains an image saved in the Tagged Image File Format (TIFF), a high-quality
        /// graphics format. It is often used for storing images with many colors, typically digital photos,
        /// and includes support for layers and multiple pages.
        /// </summary>
        TIF,
        /// <summary>
        /// A TIFF file is a graphics container that stores raster images in the Tagged Image File Format
        /// (TIFF). It contains high-quality graphics that support color depths from 1 to 24-bit and supports
        /// both lossy and lossless compression. TIFF files also support multiple layers and pages.
        /// </summary>
        TIFF,
        /// <summary>
        /// A DNG file is a RAW camera image saved in the Digital Negative (DNG) format. It stores
        /// uncompressed image data captured by various camera models, such as Hasselblad, Pentax, and Leica.
        /// Professional photographers commonly use DNG files because they maintain high image quality.
        /// </summary>
        DNG,
        /// <summary>
        /// A GIF file is an image saved in the Graphical Interchange Format (GIF). GIF images may contain
        /// up to 256 indexed colors, with a color palette that may be a predefined set of colors or may be
        /// adapted to the colors in the image. They are are saved in a lossless format, meaning that the
        /// GIF format does not compromise image's clarity is not compromised with GIF compression.
        /// </summary>
        GIF,
        /// <summary>
        /// An HEIC file contains one or more images saved in the High Efficiency Image Format (HEIF), a
        /// file format most commonly used to store photos on iOS devices. It contains an image or sequence
        /// of images likely created by an iPhone or iPad's Camera app, as well as metadata describing each
        /// image. HEIC files are most commonly saved with the .heic extension but may also be saved as.HEIF
        /// files.
        /// </summary>
        HEIC,
        /// <summary>
        /// An ARW file is a digital photograph captured by various Sony digital cameras, such as the a6300
        /// and a7R III. It contains raw, uncompressed image data as captured by the camera's CCD and is
        /// saved in the Sony Alpha Raw (ARW) format based on the TIFF specification.
        /// </summary>
        ARW,
        /// <summary>
        /// An NEF file is a raw photo captured by a Nikon digital camera. It is saved in an uncompressed
        /// Nikon Electronic Format (NEF), which stores data exactly as it was captured by the CCD. NEF
        /// files also contain image metadata, which includes the camera ID and the lens that was used.
        /// </summary>
        NEF,
        /// <summary>
        /// A CR2 file is a raw camera image created by various Canon digital cameras. It stores uncompressed
        /// image data exactly how it was captured by the CCD. Professional photographers typically use CR2
        /// files because they store higher-quality images.
        /// </summary>
        CR2,
        /// <summary>
        /// An RW2 file is a raw camera image created by a Panasonic LUMIX digital camera, such as the LX3
        /// and LX5. It contains a RAW raster image as captured by the camera sensor. RW2 files are similar
        /// to .RAW and .RWL formats and are based on the TIFF specification.
        /// </summary>
        RW2,
        /// <summary>
        /// Digital photo taken by a Samsung digital camera; contains a RAW image captured by the digital
        /// camera in a proprietary format; commonly touched up in photo editing software before publishing
        /// to the Web or to a digital media publication.
        /// </summary>
        SRW,
        /// <summary>
        /// An ORF file is a RAW photo taken with an Olympus digital camera. It is saved in the Olympus
        /// RAW Format (ORF), which stores image data exactly as captured by the camera's sensor. ORF files
        /// also include color, saturation, contrast, and temperature data and metadata detailing the
        /// specifications of the camera that captured the ORF image.
        /// </summary>
        ORF,
        /// <summary>
        /// A PJP file is a bitmap image created in the Progressive JPEG format (PJPEG). It contains encoded
        /// bitmap data so that the downloaded image appears progressively from a blurry picture to the final
        /// sharp image. This enables image previews to be viewed before the entire image is downloaded.
        /// </summary>
        PJP,
        /// <summary>
        /// A PJPG file is a raster image saved in the Progressive JPEG format. It contains encoded bitmap
        /// data that enables the image to render in multiple scans from a blurry picture to a sharp image
        /// as it is downloaded. PJPG files allow image viewing programs to show previews before the final
        /// image data is received.
        /// </summary>
        PJPG,
        /// <summary>
        /// A PJPEG file is a bitmap image created in the Progressive JPEG format (PJPEG). It contains
        /// encoded bitmap data so that the downloaded image appears progressively from a blurry picture
        /// to the final sharp image. PJPEG files may also fill alternating horizontal lines until the
        /// full picture is revealed.
        /// </summary>
        PJPEG
    }

    #endregion
}
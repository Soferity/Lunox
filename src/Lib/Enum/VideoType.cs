namespace Lunox.Library.Enum
{
    #region VideoTypeEnum

    /// <summary>
    /// 
    /// </summary>
    public enum VideoType
    {
        /// <summary>
        /// An MP4 file is a multimedia file that stores a movie or video clip and may also contain
        /// subtitles, images, and metadata. It is one of the most common formats used for distributing
        /// video online, whether it be streaming or sharing videos.
        /// </summary>
        MP4,
        /// <summary>
        /// An AVI file is a video file saved in the Audio Video Interleave (AVI) multimedia container
        /// format created by Microsoft. It stores video and audio data encoded in various codecs,
        /// including DivX and XviD.
        /// </summary>
        AVI,
        /// <summary>
        /// An HEVC file contains a video stored in the High Efficiency Video Coding (HEVC) format.
        /// This format, also known as H.265, improves over the H.264 standard by allowing videos to
        /// be stored with a lower file size but with the same video quality. HEVC helps users store
        /// more videos on their devices and also substantially reduces the file size of high-resolution
        /// videos such as 4K and 8K videos.
        /// </summary>
        HEVC,
        /// <summary>
        /// A WMV file is a video stored in the Microsoft Advanced Systems Format (ASF) and compressed
        /// with Windows Media Video compression. It is meant to be played in Microsoft media players,
        /// such as Windows Media Player and Microsoft Movies & TV. WMV files may be encrypted for use
        /// with Digital Rights Management (DRM) systems.
        /// </summary>
        WMV,
        /// <summary>
        /// An MPEG file is a video file that uses a digital video format standardized by the Moving
        /// Picture Experts Group (MPEG). It contains video and audio that are compressed using MPEG-1
        /// or MPEG-2 compression. MPEG files are typically used to share videos over the Internet.
        /// </summary>
        MPEG,
        /// <summary>
        /// Video file compressed using the MPEG-1 codec; may include both audio and video, though the
        /// audio track may use a separate type of compression; can be opened and played back by most
        /// media players
        /// </summary>
        MPEG1,
        /// <summary>
        /// An MPEG2 file is a video file encoded using the MPEG-2 codec, which is typically used to
        /// compress over-the-air, satellite, and cable TV transmissions, as well as DVD video. It is
        /// compressed using lossy compression, which significantly reduces the size of the video and
        /// audio data it contains. MPEG-2 compression is less efficient than H.264 (.H264) compression.
        /// </summary>
        MPEG2,
        /// <summary>
        /// An MPEG4 file is a video file saved in the MPEG-4 container format. It includes both audio
        /// and video data and supports multiple A/V codecs. MPEG4 files are commonly used for distributing
        /// video content over the web and for streaming videos on the Internet.
        /// </summary>
        MPEG4
    }

    #endregion
}
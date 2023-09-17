using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Shockzio.Design.Images;
using GeometryConverter = Shockzio.Design.Geometries.GeometryConverter;

namespace Shockzio.Wpf.Controls;

public class ShockzIcon : ContentControl
{
  public static DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(IconMode), typeof(ShockzIcon), new PropertyMetadata(IconMode.None));
  public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(IconType), typeof(ShockzIcon), new PropertyMetadata(IconType.None, IconPropertyChanged));
  public static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageType), typeof(ShockzIcon), new PropertyMetadata(ImageType.None, ImagePropertyChanged));
  public static DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(SolidColorBrush), typeof(ShockzIcon), new PropertyMetadata(Brushes.Silver));
  public static DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(ShockzIcon), new PropertyMetadata(null));
  public static DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ShockzIcon), new PropertyMetadata(null));

  public IconMode Mode
  {
    get => (IconMode)GetValue(ModeProperty);
    set => SetValue(ModeProperty, value);
  }

  public IconType Icon
  {
    get => (IconType)GetValue(IconProperty);
    set => SetValue(IconProperty, value);
  }

  public ImageType Image
  {
    get => (ImageType)GetValue(ImageProperty);
    set => SetValue(ImageProperty, value);
  }

  public SolidColorBrush Fill
  {
    get => (SolidColorBrush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  public Geometry Data
  {
    get => (Geometry)GetValue(DataProperty);
    set => SetValue(DataProperty, value);
  }

  public ImageSource Source
  {
    get => (ImageSource)GetValue(SourceProperty);
    set => SetValue(SourceProperty, value);
  }

  private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    ShockzIcon icon = (ShockzIcon)d;
    string geometryData = GeometryConverter.GetData(icon.ToString());

    icon.Data = Geometry.Parse(geometryData);
    icon.Mode = IconMode.Icon;
  }

  private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    ShockzIcon icon = (ShockzIcon)d;
    string base64String = ImageConverter.GetData(icon.Image.ToString());

    byte[] byteData = Convert.FromBase64String(base64String);

    BitmapImage bitmap = new();
    bitmap.BeginInit();
    bitmap.StreamSource = new MemoryStream(byteData);
    bitmap.EndInit();

    icon.Source = bitmap;
    icon.Mode = IconMode.Image;
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
  }

  static ShockzIcon()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ShockzIcon), new FrameworkPropertyMetadata(typeof(ShockzIcon)));
  }
}

public enum ImageType
{
  None,
  AppStore,
  Arsenal,
  Benz,
  Bmw,
  Chelsea,
  CrystalPalace,
  Disney,
  DisneyPlus,
  Everton,
  Honda,
  Hyundai,
  LeicesterCity,
  ManchesterCity,
  ManchesterUnited,
  NewCastle,
  Porsche,
  Prime,
  QQ,
  SouthHampton,
  Spotify,
  Sunderland,
  SwanseaCity,
  Tesla,
  Tinder,
  Tottenham,
  WestBromwitchAlbion,
}

public enum IconType
{
  None,
  Twitter,
  CheckDecagram,
  Email,
  EmailOutline,
  BellOutline,
  DotsHorizontal,
  Instagram,
  Facebook,
  Linkedin,
  Youtube,
  Link,
  LinkBox,
  LinkVariant,
  Domain,
  MapMaker,
  MapMarkerOutline,
  Microsoft,
  Apple,
  Google,
  Netflix,
  Star,
  AccountMultipleOutline,
  Image,
  Heart,
  HeartOutline,
  Pin,
  CardMultiple,
  CardMultipleOutline,
  Comment,
  CommentOutline,
  Close,
  CheckCircle,
  Check,
  Crop,
  MicrosoftVisualStudio,
  MoveOpenPlay,
  Menu,
  GoogleTranslate,
  EyedropperVariant,
  CogRefreshOutline,
  MonitorShimmer,
  ChevronRight,
  ChevronDown,
  CursorPointer,
  CalendarMonth,
  Minimize,
  Web,
  Palette,
  Contentpaste,
  Checkbold,
  FolderOpenOutline,
  FolderOpen,
  FolderRable,
  Maximize,
  Resize,
  SelectAll,
  ArrowLeftBold,
  ArrowRightBold,
  ArrowUpBold,
  ConsoleLine,
  Plus,
  ArrowAll,
  MicrosoftWindows,
  ArrowDownBox,
  TextBox,
  Folder,
  FolderOutline,
  DesktopClassic,
  Harddisk,
  File,
  FileWord,
  FileCheck,
  FileZip,
  FilePdf,
  FileImage,
  DotsHorizontalCircle,
  Home,
  HomeOutline,
  PlusBox,
  Database,
  DatabasePlus,
  Delete,
  Grid,
  AccountGroup,
  PokerChip,
  Creditcardchip,
  CreditcardchipOutline,
  Memory,
  Account,
  HomeCircle,
  HomeCircleOutline,
  Cash,
  Cash100,
  CashMulti,
  History,
}

public enum IconMode
{
  None,
  Icon,
  Image,
}
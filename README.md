# ApartmanYonetimi
Birden fazla Sitenin ve Apartmanın yönetimini sağlayan bir yazılımdır. Her site yada apartman için ayrı yönetici, kat maliki, oturan, apartman görevlisi tanımlanabilir. Admin tarafından öncelikle Sitenin ve Apartmanın tanımlanması gerekir. Yazılım üzerinden aidat takibi, site giderleri ve kasa takibi yapılır.



SQL Script




USE [master]
GO
/****** Object:  Database [ApartmanYonetimi]    Script Date: 15.12.2022 01:45:21 ******/
CREATE DATABASE [ApartmanYonetimi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ApartmanYonetimi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ApartmanYonetimi.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ApartmanYonetimi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ApartmanYonetimi_log.ldf' , SIZE = 4224KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ApartmanYonetimi] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApartmanYonetimi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ApartmanYonetimi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET ARITHABORT OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ApartmanYonetimi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ApartmanYonetimi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ApartmanYonetimi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ApartmanYonetimi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET RECOVERY FULL 
GO
ALTER DATABASE [ApartmanYonetimi] SET  MULTI_USER 
GO
ALTER DATABASE [ApartmanYonetimi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ApartmanYonetimi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ApartmanYonetimi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ApartmanYonetimi] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ApartmanYonetimi', N'ON'
GO
USE [ApartmanYonetimi]
GO
/****** Object:  User [ApartmanYonetimi]    Script Date: 15.12.2022 01:45:21 ******/
CREATE USER [ApartmanYonetimi] FOR LOGIN [ApartmanYonetimi] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ApartmanYonetimi]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Apartment]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlockId] [int] NULL,
	[SiteId] [int] NOT NULL,
	[No] [varchar](64) NOT NULL,
	[HouseSize] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Apartment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApartmentCleaning]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApartmentCleaning](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApartmentCleaningTypeId] [int] NOT NULL,
	[CreatePersonId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [varchar](1048) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_ApartmentCleaning] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApartmentCleaningType]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApartmentCleaningType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[SiteId] [int] NULL,
	[SiteType] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_ApartmentCleaningType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [varchar](16) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [varchar](16) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Block]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Block](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Block] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collecting]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collecting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApartmentId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[SafeTypeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Money] [decimal](8, 2) NOT NULL,
	[Description] [varchar](128) NULL,
	[IsRender] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Collecting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[PhoneNumber] [char](10) NOT NULL,
	[PersonName] [varchar](64) NOT NULL,
	[Adress] [varchar](256) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
	[CreatePersonId] [int] NOT NULL,
	[PaymentTypeId] [int] NULL,
	[Date] [datetime] NOT NULL,
	[Money] [decimal](10, 2) NOT NULL,
	[Description] [varchar](512) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentType]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](128) NOT NULL,
	[SiteId] [int] NULL,
	[SiteType] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AspNetUserId] [nvarchar](128) NOT NULL,
	[SiteId] [int] NOT NULL,
	[ApartmentId] [int] NULL,
	[PersonTypeId] [int] NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[PhoneNumber] [char](10) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonType]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[SiteId] [int] NULL,
	[SiteType] [tinyint] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CollectingId] [int] NOT NULL,
	[RevenuesTypeId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[Money] [decimal](8, 2) NOT NULL,
	[Description] [varchar](128) NULL,
	[IsRender] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RenevuesCollecting_RS]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RenevuesCollecting_RS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RevenuesId] [int] NOT NULL,
	[CollectingId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[Money] [decimal](8, 2) NOT NULL,
	[Description] [varchar](128) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_RenevuesCollecting_RS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Revenues]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revenues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApartmentId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[RevenuesTypeId] [int] NOT NULL,
	[Description] [varchar](128) NULL,
	[Date] [datetime] NOT NULL,
	[Money] [decimal](8, 2) NOT NULL,
	[IsRender] [bit] NOT NULL,
	[CollectingStatus] [tinyint] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Revenues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RevenuesType]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RevenuesType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[SiteId] [int] NULL,
	[SiteType] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_RevenuesType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SafeType]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SafeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[SiteId] [int] NULL,
	[SiteType] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_SafeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Domain] [varchar](64) NOT NULL,
	[PhotoName] [varchar](32) NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subsistence]    Script Date: 15.12.2022 01:45:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subsistence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[Money] [decimal](8, 2) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [varchar](1048) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserName] [nvarchar](32) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifyUserName] [nvarchar](32) NULL,
 CONSTRAINT [PK_Subsistence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 15.12.2022 01:45:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15.12.2022 01:45:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15.12.2022 01:45:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 15.12.2022 01:45:21 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 15.12.2022 01:45:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 15.12.2022 01:45:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apartment] ADD  CONSTRAINT [DF_Apartment_IsActive]  DEFAULT ((100)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ApartmentCleaningType] ADD  CONSTRAINT [DF_ApartmentCleaningType_Type]  DEFAULT ((1)) FOR [SiteType]
GO
ALTER TABLE [dbo].[ApartmentCleaningType] ADD  CONSTRAINT [DF_ApartmentCleaningType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ApartmentCleaningType] ADD  CONSTRAINT [DF_ApartmentCleaningType_CreatedDate]  DEFAULT ('2020-03-10 00:00:00.000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ApartmentCleaningType] ADD  CONSTRAINT [DF_ApartmentCleaningType_CreatedUserName]  DEFAULT (N'admin') FOR [CreatedUserName]
GO
ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_Type]  DEFAULT ((1)) FOR [SiteType]
GO
ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_CreatedDate]  DEFAULT ('2020-03-10 00:00:00.000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [DF_PaymentType_CreatedUserName]  DEFAULT (N'admin') FOR [CreatedUserName]
GO
ALTER TABLE [dbo].[PersonType] ADD  CONSTRAINT [DF_PersonType_Type]  DEFAULT ((1)) FOR [SiteType]
GO
ALTER TABLE [dbo].[PersonType] ADD  CONSTRAINT [DF_PersonType_Type1]  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[PersonType] ADD  CONSTRAINT [DF_PersonType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PersonType] ADD  CONSTRAINT [DF_PersonType_CreatedDate]  DEFAULT ('2020-03-10 00:00:00.000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PersonType] ADD  CONSTRAINT [DF_PersonType_CreatedUserName]  DEFAULT (N'admin') FOR [CreatedUserName]
GO
ALTER TABLE [dbo].[Revenues] ADD  CONSTRAINT [DF_Revenues_CollectingStatus]  DEFAULT ((1)) FOR [CollectingStatus]
GO
ALTER TABLE [dbo].[RevenuesType] ADD  CONSTRAINT [DF_RevenuesType_Type]  DEFAULT ((1)) FOR [SiteType]
GO
ALTER TABLE [dbo].[RevenuesType] ADD  CONSTRAINT [DF_RevenuesType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RevenuesType] ADD  CONSTRAINT [DF_RevenuesType_CreatedDate]  DEFAULT ('2020-03-10 14:23:22.257') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[RevenuesType] ADD  CONSTRAINT [DF_RevenuesType_CreatedUserName]  DEFAULT (N'admin') FOR [CreatedUserName]
GO
ALTER TABLE [dbo].[SafeType] ADD  CONSTRAINT [DF_SafeType_SiteType]  DEFAULT ((1)) FOR [SiteType]
GO
ALTER TABLE [dbo].[SafeType] ADD  CONSTRAINT [DF_SafeType_IsActive1]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SafeType] ADD  CONSTRAINT [DF_SafeType_CreatedDate1]  DEFAULT ('2020-03-10 14:23:22.257') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SafeType] ADD  CONSTRAINT [DF_SafeType_CreatedUserName1]  DEFAULT (N'admin') FOR [CreatedUserName]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_Block] FOREIGN KEY([BlockId])
REFERENCES [dbo].[Block] ([Id])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_Block]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartment_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartment_Site]
GO
ALTER TABLE [dbo].[ApartmentCleaning]  WITH CHECK ADD  CONSTRAINT [FK_ApartmentCleaning_ApartmentCleaningType] FOREIGN KEY([ApartmentCleaningTypeId])
REFERENCES [dbo].[ApartmentCleaningType] ([Id])
GO
ALTER TABLE [dbo].[ApartmentCleaning] CHECK CONSTRAINT [FK_ApartmentCleaning_ApartmentCleaningType]
GO
ALTER TABLE [dbo].[ApartmentCleaning]  WITH CHECK ADD  CONSTRAINT [FK_ApartmentCleaning_Person] FOREIGN KEY([CreatePersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[ApartmentCleaning] CHECK CONSTRAINT [FK_ApartmentCleaning_Person]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Site]
GO
ALTER TABLE [dbo].[Collecting]  WITH CHECK ADD  CONSTRAINT [FK_Collecting_Apartment] FOREIGN KEY([ApartmentId])
REFERENCES [dbo].[Apartment] ([Id])
GO
ALTER TABLE [dbo].[Collecting] CHECK CONSTRAINT [FK_Collecting_Apartment]
GO
ALTER TABLE [dbo].[Collecting]  WITH CHECK ADD  CONSTRAINT [FK_Collecting_SafeType] FOREIGN KEY([SafeTypeId])
REFERENCES [dbo].[SafeType] ([Id])
GO
ALTER TABLE [dbo].[Collecting] CHECK CONSTRAINT [FK_Collecting_SafeType]
GO
ALTER TABLE [dbo].[Collecting]  WITH CHECK ADD  CONSTRAINT [FK_Collecting_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Collecting] CHECK CONSTRAINT [FK_Collecting_Site]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Site]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Company]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentType] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_PaymentType]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Person] FOREIGN KEY([CreatePersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Person]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Site]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Apartment] FOREIGN KEY([ApartmentId])
REFERENCES [dbo].[Apartment] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Apartment]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AspNetUsers] FOREIGN KEY([AspNetUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AspNetUsers]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_PersonType] FOREIGN KEY([PersonTypeId])
REFERENCES [dbo].[PersonType] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_PersonType]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_Site]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Collecting] FOREIGN KEY([CollectingId])
REFERENCES [dbo].[Collecting] ([Id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Collecting]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_Person]
GO
ALTER TABLE [dbo].[Receipt]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_RevenuesType] FOREIGN KEY([RevenuesTypeId])
REFERENCES [dbo].[RevenuesType] ([Id])
GO
ALTER TABLE [dbo].[Receipt] CHECK CONSTRAINT [FK_Receipt_RevenuesType]
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS]  WITH CHECK ADD  CONSTRAINT [FK_RenevuesCollecting_RS_Collecting] FOREIGN KEY([CollectingId])
REFERENCES [dbo].[Collecting] ([Id])
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS] CHECK CONSTRAINT [FK_RenevuesCollecting_RS_Collecting]
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS]  WITH CHECK ADD  CONSTRAINT [FK_RenevuesCollecting_RS_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS] CHECK CONSTRAINT [FK_RenevuesCollecting_RS_Person]
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS]  WITH CHECK ADD  CONSTRAINT [FK_RenevuesCollecting_RS_Revenues] FOREIGN KEY([RevenuesId])
REFERENCES [dbo].[Revenues] ([Id])
GO
ALTER TABLE [dbo].[RenevuesCollecting_RS] CHECK CONSTRAINT [FK_RenevuesCollecting_RS_Revenues]
GO
ALTER TABLE [dbo].[Revenues]  WITH CHECK ADD  CONSTRAINT [FK_Revenues_Apartment] FOREIGN KEY([ApartmentId])
REFERENCES [dbo].[Apartment] ([Id])
GO
ALTER TABLE [dbo].[Revenues] CHECK CONSTRAINT [FK_Revenues_Apartment]
GO
ALTER TABLE [dbo].[Revenues]  WITH CHECK ADD  CONSTRAINT [FK_Revenues_RevenuesType] FOREIGN KEY([RevenuesTypeId])
REFERENCES [dbo].[RevenuesType] ([Id])
GO
ALTER TABLE [dbo].[Revenues] CHECK CONSTRAINT [FK_Revenues_RevenuesType]
GO
ALTER TABLE [dbo].[Revenues]  WITH CHECK ADD  CONSTRAINT [FK_Revenues_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Revenues] CHECK CONSTRAINT [FK_Revenues_Site]
GO
ALTER TABLE [dbo].[Subsistence]  WITH CHECK ADD  CONSTRAINT [FK_Subsistence_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Subsistence] CHECK CONSTRAINT [FK_Subsistence_Person]
GO
ALTER TABLE [dbo].[Subsistence]  WITH CHECK ADD  CONSTRAINT [FK_Subsistence_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Subsistence] CHECK CONSTRAINT [FK_Subsistence_Site]
GO
USE [master]
GO
ALTER DATABASE [ApartmanYonetimi] SET  READ_WRITE 
GO


﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hangman.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Hangman.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Напиши цялата дума или една буква!.
        /// </summary>
        public static string enterWholeWordOrOneLet {
            get {
                return ResourceManager.GetString("enterWholeWordOrOneLet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Напиши цялата дума или една буква на кирилица!.
        /// </summary>
        public static string enterWholeWordOrOneLetOnCirilic {
            get {
                return ResourceManager.GetString("enterWholeWordOrOneLetOnCirilic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изгубени игри.
        /// </summary>
        public static string lossGames {
            get {
                return ResourceManager.GetString("lossGames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Брой познати цели думи.
        /// </summary>
        public static string numberGuessedWholeWords {
            get {
                return ResourceManager.GetString("numberGuessedWholeWords", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Познати букви до момента.
        /// </summary>
        public static string numberOfGuessedLetters {
            get {
                return ResourceManager.GetString("numberOfGuessedLetters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Брой направени буквени предложения.
        /// </summary>
        public static string numberOfTryGuessLetter {
            get {
                return ResourceManager.GetString("numberOfTryGuessLetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Изиграни игри.
        /// </summary>
        public static string playedGames {
            get {
                return ResourceManager.GetString("playedGames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Потребителско име.
        /// </summary>
        public static string userName {
            get {
                return ResourceManager.GetString("userName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Моля изчакайте да се появи съперник!.
        /// </summary>
        public static string waitForOtherPlayer {
            get {
                return ResourceManager.GetString("waitForOtherPlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Спечелени игри.
        /// </summary>
        public static string winGames {
            get {
                return ResourceManager.GetString("winGames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Грешни букви.
        /// </summary>
        public static string wrongLetters {
            get {
                return ResourceManager.GetString("wrongLetters", resourceCulture);
            }
        }
    }
}

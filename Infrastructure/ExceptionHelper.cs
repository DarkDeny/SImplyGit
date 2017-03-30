using System;
using System.Text;

namespace Infrastructure {
    public static class ExceptionHelper {
        private const string Spacing = "  ";
        public static string Unwind(this Exception exception) {
            var sb = new StringBuilder();
            var currentException = exception;
            var currentSpacings = string.Empty;
            while (null != currentException) {
                currentSpacings += Spacing;
                sb.AppendFormat(
                    "{0}Unhandled exception: ({1}) {2}\n{0}StackTrace:\n{3}\n\n",
                    currentSpacings,
                    currentException.GetType(),
                    currentException.Message,
                    currentException.StackTrace);
                currentException = currentException.InnerException;
            }

            return sb.ToString();
        }
    }
}
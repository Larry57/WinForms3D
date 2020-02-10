namespace WinForms3D {
    class PropertyChangedHelper {
        public static bool ChangeValue<T>(ref T oldValue, T newValue) {
            if(object.Equals(oldValue, newValue))
                return false;

            oldValue = newValue;
            return true;
        }
    }
}

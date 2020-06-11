using System.Collections;

namespace ObjectComparer
{
    public enum DifferenceTypes
    {
        ValueMismatch,

        TypeMismatch,

        MissedMemberInFirstObject,

        MissedMemberInSecondObject,

        MissedElementInFirstObject,

        MissedElementInSecondObject,

        NumberOfElementsMismatch
    }
}

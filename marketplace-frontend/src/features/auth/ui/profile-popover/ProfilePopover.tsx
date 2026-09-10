import styles from "./ProfilePopover.module.css";
import {Popover} from "@/shared/ui/popover";
import type {RefObject} from "react";
import {Button} from "@/shared/ui/button";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react"
import LeaveIcon from "@/shared/assets/icons/leave.svg?react"
import {useSession} from "@/entities/session";


interface ProfilePopoverProps {
    isOpen: boolean;
    onClose: () => void;
    anchorRef: RefObject<HTMLButtonElement | null>;
}

export const ProfilePopover = (
    {
        isOpen,
        onClose,
        anchorRef,
    }: ProfilePopoverProps
) => {
    const {logout} = useSession();

    return (
        <Popover
            className={styles.profilePopover}
            isOpen={isOpen}
            onClose={onClose}
            anchorRef={anchorRef}
            placement={"bottom"}
        >
            <Button
                fullWidth
                variant={"popover"}
                size={"small"}
                icon={<ProfileIcon/>}
            >
                Личный кабинет
            </Button>

            <Button
                className={styles.leaveButton}
                fullWidth
                variant={"popover"}
                size={"small"}
                icon={<LeaveIcon/>}
                onClick={async () => {
                    onClose();
                    await logout()
                }}
            >
                Выйти
            </Button>
        </Popover>
    );
}
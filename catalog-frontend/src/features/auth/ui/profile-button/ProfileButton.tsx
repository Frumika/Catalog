import {NavButton} from "@/shared/ui/nav-button";
import {type ButtonHTMLAttributes, type RefObject, useRef} from "react";
import {type ComponentDisplayMode, useDisclosure} from "@/shared/lib";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import {useUser} from "@/entities/user/model/useUser.ts";
import {useIsAuthenticated} from "@/entities/session";
import {AuthModal} from "@/features/auth/ui/auth-modal/AuthModal.tsx";
import {ProfilePopover} from "@/features/auth/ui/profile-popover/ProfilePopover.tsx";


interface ProfileButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    displayMode?: ComponentDisplayMode;
    ref?: RefObject<HTMLButtonElement | null>
}

export const ProfileButton = (
    {
        displayMode = "full",
        className,
        onClick,
        ref,
        ...props
    }: ProfileButtonProps
) => {

    const anchorRef = useRef<HTMLButtonElement>(null);
    const isAuthenticated = useIsAuthenticated();

    const {isOpen: isModalOpen, open: openModal, close: closeModal} = useDisclosure();
    const {isOpen: isPopoverOpen, toggle: togglePopover, close: closePopover} = useDisclosure();
    const {user, isLoading: isUserLoading} = useUser(isAuthenticated);

    const displayContent = (() => {
        if (!isAuthenticated || isUserLoading || !user) return "Войти";
        return user.login;
    })();

    const handleClick = isAuthenticated ? togglePopover : openModal;

    return (
        <>
            <NavButton
                ref={anchorRef}
                {...props}
                className={className}
                badgeVisible={!isAuthenticated}
                displayMode={displayMode}
                icon={<ProfileIcon/>}
                onClick={handleClick}
            >
                {displayContent}
            </NavButton>

            <AuthModal
                isOpen={isModalOpen}
                onClose={closeModal}
            />

            <ProfilePopover
                isOpen={isPopoverOpen}
                onClose={closePopover}
                anchorRef={anchorRef}
            />
        </>
    );
}
import {NavButton} from "@/shared/ui/nav-button";
import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";
import {AuthModal} from "@/features/profile-button/ui/auth-modal/AuthModal.tsx";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";


export interface ProfileButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    badgeValue?: number;
    badgeVisible?: boolean;
    displayMode?: ComponentDisplayMode;
}

export const ProfileButton = (
    {

        children,
        displayMode = "full",
        className,
        ...props
    }: ProfileButtonProps
) => {

    const hasChildren = !!children;
    const displayContent = hasChildren ? children : "Войти";

    return (
        <>
            <NavButton
                {...props}
                badgeVisible={!hasChildren}
                displayMode={displayMode}
                icon={<ProfileIcon/>}
            >
                {displayContent}
            </NavButton>


            <AuthModal isOpen={false}/>
        </>

    );
}
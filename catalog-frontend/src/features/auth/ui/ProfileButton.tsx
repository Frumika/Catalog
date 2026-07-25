import {NavButton} from "@/shared/ui/nav-button";
import {type ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import {useAuthModal} from "@/features/auth/model/useAuthModal.ts";
import {useUser} from "@/entities/user/model/useUser.ts";
import {useIsAuthenticated, useSession} from "@/entities/session";
import {AuthModal} from "@/features/auth/ui/AuthModal.tsx";
import * as React from "react";


interface ProfileButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    displayMode?: ComponentDisplayMode;
    ref?: React.RefObject<HTMLButtonElement | null>
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

    const isAuthenticated = useIsAuthenticated();

    const {isCodeSend, sendCode, verify} = useSession();
    const {isOpen, open, close,} = useAuthModal(isAuthenticated);
    const {user, isLoading: isUserLoading} = useUser(isAuthenticated);

    const displayContent = (() => {
        if (!isAuthenticated) return "Войти";
        if (isUserLoading || !user) return "Войти";
        return user.login;
    })();

    const badgeVisible = !isAuthenticated;
    const calledMethod = isAuthenticated ? onClick : open;

    return (
        <>
            <NavButton
                ref={ref}
                {...props}
                badgeVisible={badgeVisible}
                displayMode={displayMode}
                icon={<ProfileIcon/>}
                onClick={calledMethod}
            >
                {displayContent}
            </NavButton>


            <AuthModal
                isOpen={isOpen}
                onClose={close}
                isCodeSend={isCodeSend}
                sendCode={sendCode}
                verify={verify}/>
        </>

    );
}
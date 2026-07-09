import {NavButton} from "@/shared/ui/nav-button";
import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";
import {AuthModal} from "@/features/profile-button/ui/auth-modal/AuthModal.tsx";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import {useAuthModal} from "@/features/profile-button/model/useAuthModal.ts";
import {useUser} from "@/entities/user/model/useUser.ts";
import {useIsAuthenticated, useSession} from "@/entities/session";


export interface ProfileButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    badgeValue?: number;
    badgeVisible?: boolean;
    displayMode?: ComponentDisplayMode;
}

export const ProfileButton = (
    {
        displayMode = "full",
        className,
        ...props
    }: ProfileButtonProps
) => {

    const isAuthenticated = useIsAuthenticated();

    const {isCodeSend, sendCode, verify} = useSession()
    const {isOpen, open, close,} = useAuthModal(isAuthenticated);
    const {user, isLoading: isUserLoading} = useUser(isAuthenticated);

    const displayContent = (() => {
        if (!isAuthenticated) return "Войти";
        if (isUserLoading || !user) return "";
        return user.login;
    })();

    const badgeVisible = !isAuthenticated;

    return (
        <>
            <NavButton
                {...props}
                badgeVisible={badgeVisible}
                displayMode={displayMode}
                icon={<ProfileIcon/>}
                onClick={open}
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
import {NavButton} from "@/shared/ui/nav-button";
import type {ButtonHTMLAttributes} from "react";
import type {ComponentDisplayMode} from "@/shared/lib";
import {AuthModal} from "@/features/profile-button/ui/auth-modal/AuthModal.tsx";
import ProfileIcon from "@/shared/assets/icons/profile.svg?react";
import {useAuthModal} from "@/features/profile-button/model/useAuthModal.ts";


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

    const {
        isOpen,
        session,
        isVerify,
        isCodeSend,
        sendCode,
        verify,
        logout,
        logoutAll,
        open,
        close,
    } = useAuthModal();

    const hasChildren = !!session;
    const displayContent = hasChildren ? session.login : "Войти";

    return (
        <>
            <NavButton
                {...props}
                badgeVisible={!hasChildren}
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